using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TetrisVisualManager : MonoBehaviour
{
    private Timing timing;

    [SerializeField]
    private TextMeshProUGUI bombTimerText;

    private void Awake(){
        this.timing = FindObjectOfType<Timing>();
    }

    private void Update(){
        if (timing.currentTime <= 60f){
            bombTimerText.text = Mathf.Floor(60f - timing.currentTime).ToString();
        } else {
            bombTimerText.text = "0";
        }
    }



}
