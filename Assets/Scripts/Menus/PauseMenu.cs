using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;

    // Ui GameObject
    [SerializeField]
    private GameObject pauseMenuUI;

    // Buttons
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button restartButton;

    public event EventHandler ResumeGameEvent;
    public event EventHandler BackToStartMenu;
    public event EventHandler RestartGameEvent;

    private void Awake(){
        resumeButton.onClick.AddListener(() => { GameManager.Singleton.PauseGame(); } );
        exitButton.onClick.AddListener(() => { ExitGame(); });
        restartButton.onClick.AddListener(() => { RestartGame(); });
        gameManager = GameManager.Singleton;
    }

    private void OnEnable(){
        gameManager.ChangePauseMenuState += When_ChangePauseMenuState;
    }

    private void OnDisable(){
        gameManager.ChangePauseMenuState -= When_ChangePauseMenuState;
    }

    private void When_ChangePauseMenuState(object sender, EventArgs e){
        if (!GameManager.gameIsPaused){
            pauseMenuUI.SetActive(true);
        } else {
            pauseMenuUI.SetActive(false);
        }
    }

    private void ExitGame(){
        BackToStartMenu?.Invoke(this, EventArgs.Empty);
    }

    private void RestartGame(){
        pauseMenuUI.SetActive(false);
        RestartGameEvent?.Invoke(this, EventArgs.Empty);
    }

}
