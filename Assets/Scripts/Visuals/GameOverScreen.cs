using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject countdownScreen;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button quitButton;
    private Board board;

    public event EventHandler RestartGameEvent;
    public event EventHandler QuitGameEvent;


    private void Awake(){
        restartButton.onClick.AddListener(() => { RestartGame(); });
        quitButton.onClick.AddListener(() => { QuitGame(); });
    }

    private void OnEnable(){
        this.board = DependencyManager.instance.board;
        board.GameOverEvent += When_GameOverEvent;
    }

    private void When_GameOverEvent(object sender, EventArgs e){
        countdownScreen.SetActive(true);
    }

    private void RestartGame(){
        RestartGameEvent?.Invoke(this, EventArgs.Empty);
    }

    private void QuitGame(){
        QuitGameEvent?.Invoke(this, EventArgs.Empty);
    }
}


