using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.Netcode;


public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Dropdown characterSelecter;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject MultiplayerMenu;

    private GameManager gameManager;


    private int character;

    private void Awake(){
        gameManager = GameManager.Singleton;

        backButton.onClick.AddListener(() => { When_BackButtonPressed(); });
        characterSelecter.onValueChanged.AddListener(gameManager.ChangeCharacter);

    }

    private void When_BackButtonPressed(){
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        
    }






}
