using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

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
    private GameManager gameManager;
    private NetworkGameManager networkGameManager;
    [SerializeField]
    private TextMeshProUGUI gameOverText;

    public event EventHandler RestartGameEvent;
    public event EventHandler BackToStartMenu;


    private void Awake(){
        gameManager = GameManager.Singleton;
        networkGameManager = NetworkGameManager.Singleton;
        board = FindObjectOfType<Board>();
        health = FindObjectOfType<Health>();

        restartButton.onClick.AddListener(() => { RestartGame(); });
        quitButton.onClick.AddListener(() => { gameManager.BackToStartaMenu(); });

        
    }

    private void OnEnable(){
        networkGameManager.GameOverEvent += When_GameOver;
    }

    private void OnDisable(){
        networkGameManager.GameOverEvent -= When_GameOver;
    }

    private void When_GameOver(object sender, NetworkGameManager.GameOverEventArgs e){
        gameOverScreen.SetActive(true); 
        if (e.won){
            gameOverText.text = "You won";
        } else {
            gameOverText.text = "You lost";
        }
    }

    public void OpenGameOverScreen(bool won){
        gameOverScreen.SetActive(true); 
        if (won){
            gameOverText.text = "You won";
        } else {
            gameOverText.text = "You lost";
        }
    }

    private void RestartGame(){
        networkGameManager.RestartGameServerRPC();
        gameOverScreen.SetActive(false);
    }


}


