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
    private TMPro.TMP_Dropdown modeSelector;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button serverButton;
    [SerializeField]
    private Button hostButton;
    [SerializeField]
    private Button clientButton;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject MultiplayerMenu;


    private int character;

    public event EventHandler<ChangeCharacterEventEventArgs> ChangeCharacterEvent;
    public event EventHandler<ChangeModeEventEventArgs> ChangeModeEvent;

    public class ChangeCharacterEventEventArgs : EventArgs {
        public int id;
    }

    public class ChangeModeEventEventArgs : EventArgs {
        public GameManager.GameType id;
    }

    private void Awake(){
        InitializeDropdown();
        backButton.onClick.AddListener(() => { When_BackButtonPressed(); });
        serverButton.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); });
        hostButton.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); });
        clientButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
        characterSelecter.onValueChanged.AddListener(ChangeCharacter);
        modeSelector.onValueChanged.AddListener(ChangeGamemode);

    }

    private void ChangeCharacter(int value){
        ChangeCharacterEvent?.Invoke(this, new ChangeCharacterEventEventArgs { id = value } );
    }

    private void ChangeGamemode(int value){
        ChangeModeEvent?.Invoke(this, new ChangeModeEventEventArgs { id = (GameManager.GameType)value });
        if (value == 0){
            MultiplayerMenu.SetActive(false);
        } else if (value == 1){
            MultiplayerMenu.SetActive(true);
        }
    }

    private void InitializeDropdown(){
        int characterCount = CharacterData.Characters.Count;

        for (int i = 0; i < characterCount; i ++){
            characterSelecter.options.Add(new TMPro.TMP_Dropdown.OptionData { text = CharacterData.Characters[i].name });
        }

        foreach (GameManager.GameType options in Enum.GetValues(typeof(GameManager.GameType))){
            modeSelector.options.Add(new TMPro.TMP_Dropdown.OptionData { text = options.ToString() });
        }

    }

    private void When_BackButtonPressed(){
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        
    }






}
