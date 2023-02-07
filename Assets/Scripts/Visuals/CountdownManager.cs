using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    [SerializeField]
    private GameObject countdownScreen;
    [SerializeField]
    private TextMeshProUGUI countdownText;

    private int timer;
    private float lastTime;

    public event EventHandler StartGame;

    private void OnEnable(){
        timer = 3;
        countdownText.text = timer.ToString();
        lastTime = Time.time;
    }

    void Update(){
        if (Time.time > lastTime + 1f){
            timer --;
            countdownText.text = timer.ToString();
            lastTime = Time.time;
        }

        if (timer <= 0){
            StartGame?.Invoke(this, EventArgs.Empty);
            countdownScreen.SetActive(false);
        }
    }
}
