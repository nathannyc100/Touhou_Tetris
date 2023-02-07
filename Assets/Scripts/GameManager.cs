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
    private CountdownScreen countdownScreen;
    private GameOverScreen gameOverScreen;
    private Board board;

    public static GameState GameCurrentState;
    [System.NonSerialized]
    public int character;
    [System.NonSerialized]
    public string text;

    public static bool gameIsPaused;
    public GameType gameType = GameType.Singleplayer;

    public event EventHandler ChangePauseMenuState;
    public event EventHandler ResetGame;

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

    private void When_CountdownFinished(object sender, EventArgs e){
        GameCurrentState = GameState.Tetris;
        countdownScreen.CountdownFinished -= When_CountdownFinished;
    }

    private void When_GameOverEvent(object sender, EventArgs e){
        GameCurrentState = GameState.GameOver;
    }

    private void When_RestartGameEvent(object sender, EventArgs e){
        GameCurrentState = GameState.CountdownScreen;
        countdownScreen.CountdownFinished += When_CountdownFinished;
    }

    private void When_QuitGameEvent(object sender, EventArgs e){

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
        

        switch (GameCurrentState){
            case GameState.CountdownScreen:
                this.controlsManager = DependencyManager.instance.controlsManager;
                this.pauseMenu = DependencyManager.instance.pauseMenu;
                this.countdownScreen = DependencyManager.instance.countdownScreen;
                this.gameOverScreen = DependencyManager.instance.gameOverScreen;
                this.board = DependencyManager.instance.board;

                countdownScreen.CountdownFinished += When_CountdownFinished;
                board.GameOverEvent += When_GameOverEvent;
                break;

            default :
                break;
        }

    }

    public void RunOnFirstFrame(){
        if (GameCurrentState == GameState.CountdownScreen){
            ResetGame?.Invoke(this, EventArgs.Empty);
        }

    }

}
