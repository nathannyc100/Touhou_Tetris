using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    public enum GameState {
        StartMenu,
        CountdownScreen,
        Tetris,
        GameOver,
    }

    public enum GameType {
        Singleplayer,
        Multiplayer,
    }

    private void Awake(){
        MakeSingleton();
        GameCurrentState = GameState.StartMenu;
        GameCurrentMode = GameType.Singleplayer;

        this.mainMenu = FindObjectOfType<MainMenu>();
        this.optionsMenu = FindObjectOfType<OptionsMenu>();
    }

    private void OnEnable(){
        SceneManager.activeSceneChanged += When_SceneLoaded;
        mainMenu.StartGame += When_StartGame;
        optionsMenu.ChangeCharacterEvent += When_ChangeCharacterEvent;
        optionsMenu.ChangeModeEvent += When_ChangeModeEvent;
    }

    private void OnDisable(){
        SceneManager.activeSceneChanged -= When_SceneLoaded;
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
            LoadNextScene("Tetris");

        }
    }

    private void When_SceneLoaded(Scene previousScene, Scene currentScene){
        string previousSceneName = previousScene.name;
        string sceneName = currentScene.name;

        ReloadGameManagerDependencies();

        switch (sceneName){
            case "Tetris" :
                InitializeNetworkScript?.Invoke(this, EventArgs.Empty);

                controlsManager.OnPausePressed += When_OnPausePressed;
                pauseMenu.ResumeGameEvent += When_OnPausePressed;
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
                mainMenu.StartGame += When_StartGame;
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

    private void When_OnPausePressed(object sender, EventArgs e){
        if (GameCurrentMode == GameType.Multiplayer){
            return;
        }

        if (gameIsPaused){
            ResumeGame();
        } else {
            PauseGame();
        }


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

    private void PauseGame(){
        ChangePauseMenuState?.Invoke(this, EventArgs.Empty);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private void ResumeGame(){
        ChangePauseMenuState?.Invoke(this, EventArgs.Empty);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void ReloadGameManagerDependencies(){

        switch (GameCurrentState){
            case GameState.StartMenu :
                this.mainMenu = FindObjectOfType<MainMenu>();
                this.optionsMenu = FindObjectOfType<OptionsMenu>();
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
                controlsManager.OnPausePressed -= When_OnPausePressed;
                pauseMenu.ResumeGameEvent -= When_OnPausePressed;
                countdownScreen.CountdownFinished -= When_CountdownFinished;
                board.GameOverEvent -= When_GameOverEvent;
                health.GameOverEvent -= When_GameOverEvent;
                gameOverScreen.RestartGameEvent -= When_RestartGameEvent;
                pauseMenu.BackToStartMenu -= When_BackToStartMenu;
                pauseMenu.RestartGameEvent -= When_RestartGameEvent;
                gameOverScreen.BackToStartMenu -= When_BackToStartMenu;
                break;
            
            case "StartMenu" :
                mainMenu.StartGame -= When_StartGame;
                optionsMenu.ChangeCharacterEvent -= When_ChangeCharacterEvent;
                break;

            default :
                break;
        }

        SceneManager.LoadScene(sceneName);

    }

}
