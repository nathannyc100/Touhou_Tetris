using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button singleplayerButton;
    [SerializeField]
    private Button multiplayerButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionsMenu;

    public event EventHandler OpenLobbyMenu;

    private void Awake(){
        singleplayerButton.onClick.AddListener(() => { StartSingleplayerGame(); });
        multiplayerButton.onClick.AddListener(() => { StartMultiplayerGame(); });
        optionsButton.onClick.AddListener(() => { When_OptionsButtonClicked(); });
    }

    private void When_OptionsButtonClicked(){
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    

    public void StartSingleplayerGame(){
        GameManager.instance.StartGame();
    }

    public void StartMultiplayerGame(){
        mainMenu.SetActive(false);
        OpenLobbyMenu?.Invoke(this, EventArgs.Empty);
    }

    public void QuitGame(){
        Application.Quit();
    }

}
