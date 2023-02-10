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
    private float lastTime;
    private float startTime;
    public float increment = 1f;
    private bool gameIsRunning = false;

    private void Awake(){
        this.board = FindObjectOfType<Board>();
        this.gameManager = GameManager.instance;
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
        }
    }

    private void When_ResetGame_StartTiming(object sender, EventArgs e){
        startTime = Time.time;
        lastTime = startTime;
        time = 0f;
        gameIsRunning = true;
    }

    private void IncrementTime(){
        time += increment;
        lastTime = Time.time;
        TimeIncrement?.Invoke(this, new TimeIncrementEventArgs { time = this.time } );
    }


}
