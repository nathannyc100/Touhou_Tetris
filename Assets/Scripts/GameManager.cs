using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    // Menu scene
    private MainMenu mainMenu;
    private OptionsMenu optionsMenu;
    private CharacterManager characterManager;

    // Tetris scene
    private ControlsManager controlsManager;
    private PauseMenu pauseMenu;
    private CountdownScreen countdownScreen;
    private GameOverScreen gameOverScreen;
    private Board board;
    private Health health;
    private LobbyMenu lobbyMenu;
    

    // Networking
    private NetworkGameManager networkGameManager;

    private string GameCurrentScene;
    public static GameState GameCurrentState;
    [System.NonSerialized]
    public int character;
    [System.NonSerialized]
    public string text;

    public static bool gameIsPaused;
    public static GameType GameCurrentMode;

    public event EventHandler ResetGame;
    public event EventHandler InitializeNetworkScript;
    public event EventHandler MultiplayerStartCountdown;
    public event EventHandler<LoadNextSceneEventArgs> LoadNextSceneEvent;
    public event EventHandler OpenCharacterSelectMenu;

    public class LoadNextSceneEventArgs : EventArgs {
        public string sceneName;
    }

    public enum GameState {
        StartMenu,
        CountdownScreen,
        Tetris,
        GameOver,
    }

    public enum GameType {
        Singleplayer,
        Multiplayer,
        None,
    }

    public enum JoinLobbyMode {
        CreateNew,
        JoinRandom,
        JoinWithCode,
    }

    private void Awake(){
        MakeSingleton();
        GameCurrentState = GameState.StartMenu;
        GameCurrentMode = GameType.None;

        mainMenu = FindObjectOfType<MainMenu>();
        optionsMenu = FindObjectOfType<OptionsMenu>();
        lobbyMenu = FindObjectOfType<LobbyMenu>();
        characterManager = FindObjectOfType<CharacterManager>();
    }

    private void MakeSingleton(){
        if (Singleton != null){
            Destroy(gameObject);
        } else {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartGame(){
        if (GameCurrentMode == GameType.Singleplayer){
            NetworkManager.Singleton.StartHost();
        }

        if (GameCurrentMode == GameType.Multiplayer){
            
        }
        
        if (GameCurrentState == GameState.StartMenu){
            GameCurrentState = GameState.CountdownScreen;
            //LoadNextScene("Tetris");

        }
    }

    private void When_SceneLoadComplete(object sender, NetworkGameManager.SceneLoadCompleteEventArgs e){
        string sceneName = e.sceneName;

        ReloadGameManagerDependencies();

        switch (sceneName){
            case "Tetris" :
                InitializeNetworkScript?.Invoke(this, EventArgs.Empty);
                break;
            
            case "StartMenu" :
                break;

            default :
                break;
        }
    }

    public void ChangeCharacter(int value){
        character = value;
    }

    public void BackToStartaMenu(){
        GameCurrentState = GameState.StartMenu;
        LoadNextScene("StartMenu");
    }

    public void ReloadGameManagerDependencies(){

        switch (GameCurrentState){
            case GameState.StartMenu :
                mainMenu = FindObjectOfType<MainMenu>();
                optionsMenu = FindObjectOfType<OptionsMenu>();
                lobbyMenu = FindObjectOfType<LobbyMenu>();
                break;

            case GameState.CountdownScreen :
                controlsManager = FindObjectOfType<ControlsManager>();
                pauseMenu = FindObjectOfType<PauseMenu>();
                countdownScreen = FindObjectOfType<CountdownScreen>();
                gameOverScreen = FindObjectOfType<GameOverScreen>();
                board = FindObjectOfType<Board>();
                health = FindObjectOfType<Health>();
                break;

            default :
                break;
        }

    }

    private void LoadNextScene(string sceneName){
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName){
            case "Tetris" :
                break;
            
            case "StartMenu" :
                break;

            default :
                break;
        }

        //LoadNextSceneEvent?.Invoke(this, new LoadNextSceneEventArgs { sceneName = sceneName });
        //SceneManager.LoadScene(sceneName);

    }

    public void GetNetworkReference(){
        networkGameManager = NetworkGameManager.Singleton;

        networkGameManager.MultiplayerStartCountdown += When_MultiplayerStartCountdown;
        networkGameManager.SceneLoadComplete += When_SceneLoadComplete;
    }

    private void When_MultiplayerStartCountdown(object sender, EventArgs e){
        MultiplayerStartCountdown?.Invoke(this, EventArgs.Empty);
    }

    public void JoinLobby(JoinLobbyMode joinMode){
        GameManager.GameCurrentMode = GameType.Multiplayer;

        Debug.Log("join lobby");

        if (joinMode == JoinLobbyMode.JoinRandom){
            Debug.Log("random");
            NetworkManager.Singleton.StartClient();
        } else if (joinMode == JoinLobbyMode.CreateNew){
            Debug.Log("create");
            NetworkManager.Singleton.StartHost(); 
        }

        OpenCharacterSelectMenu?.Invoke(this, EventArgs.Empty);

        Debug.Log("Open menu");
    }

}
