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

    private int timer;
    private float lastTime;

    public event EventHandler CountdownFinished;

    private void Awake(){
        this.gameManager = GameManager.instance;
    }

    private void OnEnable(){
        gameManager.ResetGame += When_ResetGame;
    }

    private void OnDisable(){
        gameManager.ResetGame -= When_ResetGame;
    }

    void Update(){
        if (Time.time > lastTime + 1f){
            timer --;
            lastTime = Time.time;
            countdownText.text = timer.ToString();
        }

        if (timer <= 0){
            CountdownFinished?.Invoke(this, EventArgs.Empty);
            countdownScreen.SetActive(false);
        }
    }

    private void When_ResetGame(object sender, EventArgs e){
        countdownScreen.SetActive(true);
        timer = 3;
        lastTime = Time.time;
        countdownText.text = timer.ToString();
    }
}
