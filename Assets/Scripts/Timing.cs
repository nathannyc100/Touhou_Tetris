using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timing : MonoBehaviour
{
    private Board board;
    private GameManager gameManager;

    public event EventHandler<TimeIncrementEventArgs> TimeIncrement;

    public class TimeIncrementEventArgs : EventArgs {
        public float time;
    }

    public float time;
    public float countdownTimer;
    public float currentTime;
    public float increment = 1f;
    private bool gameIsRunning = false;
    private float skillFinalTimer = 60f;
    private float feverModeTimer = 180f;
    private float lastTime;
    private float startTime;
    

    private void Awake(){
        this.board = FindObjectOfType<Board>();
        this.gameManager = GameManager.Singleton;
    }

    private void OnEnable(){
        gameManager.ResetGame += When_ResetGame_StartTiming;
    }

    private void OnDisable(){
        gameManager.ResetGame -= When_ResetGame_StartTiming;
    }

    private void Update(){
        if (gameIsRunning == true){
            if (Time.time > lastTime + increment){
                IncrementTime();
            }

            currentTime += Time.deltaTime;
        }

        if (currentTime >= skillFinalTimer){
            UnlockFinal();
        }

        if (currentTime >= feverModeTimer){
            FeverMode();
        }

        if (countdownTimer > 0){
            countdownTimer -= Time.deltaTime;
        } else if (gameIsRunning == false){
            StartGameTimer();
        }
    }

    private void When_ResetGame_StartTiming(object sender, EventArgs e){
        countdownTimer = 3f;
        gameIsRunning = false;
    }

    private void StartGameTimer(){
        startTime = Time.time;
        lastTime = startTime;
        time = 0f;
        currentTime = 0f;
        gameIsRunning = true;
    }

    private void IncrementTime(){
        time += increment;
        lastTime = Time.time;
        TimeIncrement?.Invoke(this, new TimeIncrementEventArgs { time = this.time } );
    }

    private void UnlockFinal(){

    }

    private void FeverMode(){

    }


}
