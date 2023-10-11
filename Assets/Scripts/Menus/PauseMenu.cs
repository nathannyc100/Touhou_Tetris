using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    private NetworkGameManager networkGameManager;

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

    private void Awake(){
        gameManager = GameManager.Singleton;
        networkGameManager = NetworkGameManager.Singleton;

        //resumeButton.onClick.AddListener(() => { NetworkTimingManager.Singleton.PauseGameServerRPC(!NetworkTimingManager.Singleton.network_gameIsPaused.Value); } );
        exitButton.onClick.AddListener(() => { gameManager.BackToStartaMenu(); });
        restartButton.onClick.AddListener(() => { RestartGame(); });
    }

    public void TogglePauseMenu(bool pauseMenuOn) {
        pauseMenuUI.SetActive(pauseMenuOn);
    }

    private void RestartGame(){
        pauseMenuUI.SetActive(false);
        networkGameManager.RestartGameServerRPC();
    }

}
