using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button quitButton;
    private Board board;
    private Health health;

    public event EventHandler RestartGameEvent;
    public event EventHandler BackToStartMenu;


    private void Awake(){
        restartButton.onClick.AddListener(() => { RestartGame(); });
        quitButton.onClick.AddListener(() => { QuitGame(); });

        this.board = FindObjectOfType<Board>();
        this.health = FindObjectOfType<Health>();
    }

    private void OnEnable(){
        board.GameOverEvent += When_GameOverEvent;
        health.GameOverEvent += When_GameOverEvent;
    }

    private void OnDisable(){
        board.GameOverEvent -= When_GameOverEvent;
        health.GameOverEvent -= When_GameOverEvent;
    }

    private void When_GameOverEvent(object sender, EventArgs e){
        gameOverScreen.SetActive(true); 
    }

    private void RestartGame(){
        RestartGameEvent?.Invoke(this, EventArgs.Empty);
        gameOverScreen.SetActive(false);
    }

    private void QuitGame(){
        BackToStartMenu?.Invoke(this, EventArgs.Empty);
    }
}


