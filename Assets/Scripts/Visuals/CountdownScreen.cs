using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CountdownScreen : MonoBehaviour
{
    
    [SerializeField]
    private GameObject countdownScreen;
    [SerializeField]
    private TextMeshProUGUI countdownText;
    private GameManager gameManager;

    private void Awake(){
        this.gameManager = GameManager.Singleton;
        countdownScreen.SetActive(true);
        if (GameManager.GameCurrentMode == GameManager.GameType.Multiplayer){
            gameManager.MultiplayerStartCountdown += When_MultiplayerStartCountdown;
            countdownText.text = "Waiting for other players";
        } else {
 
        }

    }

    private void OnEnable(){
        gameManager.ResetGame += When_ResetGame;
    }

    private void OnDisable(){
        gameManager.ResetGame -= When_ResetGame;
    }

    private void When_ResetGame(object sender, EventArgs e){
        if (GameManager.GameCurrentMode == GameManager.GameType.Singleplayer){
            ResetGame();
        }
    }

    private void When_MultiplayerStartCountdown(object sender, EventArgs e){

        ResetGame();
    }

    private void ResetGame(){
        countdownScreen.SetActive(true);
    }

    public void ChangeCountdownText(string text){
        Debug.Log(text);
        countdownText.text = text;
    }

    public void DeactivateCountdownScreen(){
        countdownScreen.SetActive(false);
    }
}
