using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionsMenu;

    public event EventHandler StartGame;

    private void Awake(){
        playButton.onClick.AddListener(() => { PlayGame(); });
        optionsButton.onClick.AddListener(() => { When_OptionsButtonClicked(); });
    }

    private void When_OptionsButtonClicked(){
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    

    public void PlayGame(){

        StartGame?.Invoke(this, EventArgs.Empty);

    }

    public void QuitGame(){
        Application.Quit();
    }

}
