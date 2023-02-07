using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private OptionsMenu optionsMenu;
    [SerializeField]
    private GameObject gameManager;

    private ControlsManager controlsManager;
    private PauseMenu pauseMenu;
    private CountdownManager countdownManager;

    public static GameState GameCurrentState;
    [System.NonSerialized]
    public int character;
    [System.NonSerialized]
    public string text;

    public static bool gameIsPaused;
    public GameType gameType = GameType.Singleplayer;

    public event EventHandler ChangePauseMenuState;

    public enum GameState {
        StartMenu,
        CountdownScreen,
        Tetris,
    }

    public enum GameType {
        Singleplayer,
        Multiplayer,
    }

    

    private void Awake(){
        MakeSingleton();
        GameCurrentState = GameState.StartMenu;
    }

    private void Start(){
        SceneManager.activeSceneChanged += When_SceneLoaded;
        mainMenu.StartGame += When_StartGame;
        optionsMenu.ChangeCharacterEvent += When_ChangeCharacterEvent;
        DependencyManager.instance.DependenciesChanged += When_DependencyChanged;

    }

    private void MakeSingleton(){
        if (instance != null){
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void When_StartGame(object sender, EventArgs e){
        if (GameCurrentState == GameState.StartMenu){
            GameCurrentState = GameState.CountdownScreen;
            SceneManager.LoadScene("Tetris");

            
            
        }
    }

    private void When_StartGameStartBoard(object sender, EventArgs e){
        GameCurrentState = GameState.Tetris;
    }

    private void When_SceneLoaded(Scene previousScene, Scene currentScene){
        string sceneName = currentScene.name;
        if (sceneName == "Tetris"){
            controlsManager.OnPausePressed += When_OnPausePressed;
            pauseMenu.OnResumeGame += When_OnPausePressed;
        }
    }

    private void When_ChangeCharacterEvent(object sender, OptionsMenu.ChangeCharacterEventEventArgs e){
        this.character = e.id;
    }

    private void When_OnPausePressed(object sender, EventArgs e){
        if (gameType == GameType.Multiplayer){
            return;
        }

        if (gameIsPaused){
            ResumeGame();
        } else {
            PauseGame();
        }


    }

    private void PauseGame(){
        ChangePauseMenuState?.Invoke(this, EventArgs.Empty);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Debug.Log("Pause game");
    }

    private void ResumeGame(){
        ChangePauseMenuState?.Invoke(this, EventArgs.Empty);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void When_DependencyChanged(object sender, EventArgs e){
        this.controlsManager = DependencyManager.instance.controlsManager;
        this.pauseMenu = DependencyManager.instance.pauseMenu;

        if (GameCurrentState == GameState.CountdownScreen){
            this.controlsManager = DependencyManager.instance.controlsManager;
            this.countdownManager = DependencyManager.instance.countdownManager;

            countdownManager.StartGame += When_StartGameStartBoard;
        }

    }

}
