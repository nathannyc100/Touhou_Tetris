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


    private int character;

    public event EventHandler<ChangeCharacterEventEventArgs> ChangeCharacterEvent;

    public class ChangeCharacterEventEventArgs : EventArgs {
        public int id;
    }

    private void Awake(){
        InitializeDropdown();
        backButton.onClick.AddListener(() => { When_BackButtonPressed(); });
        characterSelecter.onValueChanged.AddListener(ChangeCharacter);

    }

    private void ChangeCharacter(int value){
        ChangeCharacterEvent?.Invoke(this, new ChangeCharacterEventEventArgs { id = value } );
    }

    private void InitializeDropdown(){
        int characterCount = CharacterData.Characters.Count;

        for (int i = 0; i < characterCount; i ++){
            characterSelecter.options.Add(new TMPro.TMP_Dropdown.OptionData { text = CharacterData.Characters[i].name });
        }
    }

    private void When_BackButtonPressed(){
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        
    }






}
