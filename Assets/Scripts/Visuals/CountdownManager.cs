using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject countdownScreen;
    [SerializeField]
    private TextMeshProUGUI countdownText;

    private int timer;
    private float lastTime;

    public event EventHandler CountdownFinished;

    private void OnEnable(){
        timer = 3;
        lastTime = Time.time;
        countdownText.text = timer.ToString();
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
}
