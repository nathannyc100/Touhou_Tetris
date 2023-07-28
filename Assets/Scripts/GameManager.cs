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

    public event EventHandler ChangePauseMenuState;
    public event EventHandler ResetGame;
    public event EventHandler InitializeNetworkScript;
    public event EventHandler MultiplayerStartCountdown;
    public event EventHandler<LoadNextSceneEventArgs> LoadNextSceneEvent;

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
    }

    private void OnEnable(){
        //SceneManager.activeSceneChanged += When_SceneLoaded;
        optionsMenu.ChangeCharacterEvent += When_ChangeCharacterEvent;
        optionsMenu.ChangeModeEvent += When_ChangeModeEvent;
    }

    private void OnDisable(){
        //SceneManager.activeSceneChanged -= When_SceneLoaded;
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

                countdownScreen.CountdownFinished += When_CountdownFinished;
                board.GameOverEvent += When_GameOverEvent;
                health.GameOverEvent += When_GameOverEvent;
                gameOverScreen.RestartGameEvent += When_RestartGameEvent;
                pauseMenu.BackToStartMenu += When_BackToStartMenu;
                pauseMenu.RestartGameEvent += When_RestartGameEvent;
                gameOverScreen.BackToStartMenu += When_BackToStartMenu;

                gameIsPaused = false;
                Time.timeScale = 1f;
                ResetGame?.Invoke(this, EventArgs.Empty);
                break;
            
            case "StartMenu" :
                optionsMenu.ChangeCharacterEvent += When_ChangeCharacterEvent;
                break;

            default :
                break;
        }
    }

    private void When_ChangeCharacterEvent(object sender, OptionsMenu.ChangeCharacterEventEventArgs e){
        this.character = e.id;
    }

    private void When_ChangeModeEvent(object sender, OptionsMenu.ChangeModeEventEventArgs e){
        GameCurrentMode = e.id;
    }

   

    private void When_CountdownFinished(object sender, EventArgs e){
        GameCurrentState = GameState.Tetris;
        countdownScreen.CountdownFinished -= When_CountdownFinished;
    }

    private void When_GameOverEvent(object sender, EventArgs e){
        GameCurrentState = GameState.GameOver;
    }

    private void When_RestartGameEvent(object sender, EventArgs e){
        gameIsPaused = false;
        Time.timeScale = 1f;
        GameCurrentState = GameState.CountdownScreen;
        ResetGame?.Invoke(this, EventArgs.Empty);
        countdownScreen.CountdownFinished += When_CountdownFinished;
    }

    private void When_BackToStartMenu(object sender, EventArgs e){
        GameCurrentState = GameState.StartMenu;
        LoadNextScene("StartMenu");
    }

    public void PauseGame(){
        if (GameCurrentMode == GameType.Multiplayer){
            return;
        }

        ChangePauseMenuState?.Invoke(this, EventArgs.Empty);
        if (gameIsPaused){
            Time.timeScale = 1f;
            gameIsPaused = false;
        } else {
            Time.timeScale = 0f;
            gameIsPaused = true;
        }
        
    }

    public void ReloadGameManagerDependencies(){

        switch (GameCurrentState){
            case GameState.StartMenu :
                mainMenu = FindObjectOfType<MainMenu>();
                optionsMenu = FindObjectOfType<OptionsMenu>();
                lobbyMenu = FindObjectOfType<LobbyMenu>();
                break;

            case GameState.CountdownScreen :
                this.controlsManager = FindObjectOfType<ControlsManager>();
                this.pauseMenu = FindObjectOfType<PauseMenu>();
                this.countdownScreen = FindObjectOfType<CountdownScreen>();
                this.gameOverScreen = FindObjectOfType<GameOverScreen>();
                this.board = FindObjectOfType<Board>();
                this.health = FindObjectOfType<Health>();
                break;

            default :
                break;
        }

    }

    private void LoadNextScene(string sceneName){
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName){
            case "Tetris" :
                countdownScreen.CountdownFinished -= When_CountdownFinished;
                board.GameOverEvent -= When_GameOverEvent;
                health.GameOverEvent -= When_GameOverEvent;
                gameOverScreen.RestartGameEvent -= When_RestartGameEvent;
                pauseMenu.BackToStartMenu -= When_BackToStartMenu;
                pauseMenu.RestartGameEvent -= When_RestartGameEvent;
                gameOverScreen.BackToStartMenu -= When_BackToStartMenu;
                break;
            
            case "StartMenu" :
                optionsMenu.ChangeCharacterEvent -= When_ChangeCharacterEvent;
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

        
        
    }

}
