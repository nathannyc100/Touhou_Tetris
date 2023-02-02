using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    
    private ControlsManager controlsManager;
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

    public event EventHandler OnResumeGame;

    private void Awake(){
        resumeButton.onClick.AddListener(() => { ResumeGame(); } );
    }

    private void Start(){
        gameManager = GameManager.instance;
        gameManager.ChangePauseMenuState += When_ChangePauseMenuState;
    }

    private void When_ChangePauseMenuState(object sender, EventArgs e){
        if (!GameManager.gameIsPaused){
            pauseMenuUI.SetActive(true);
        } else {
            pauseMenuUI.SetActive(false);
        }
    }

    private void ResumeGame(){
        OnResumeGame?.Invoke(this, EventArgs.Empty);
    }

}
