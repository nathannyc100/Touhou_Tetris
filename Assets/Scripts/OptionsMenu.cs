using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


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

    private int character;
    private TMPro.TMP_Dropdown.OptionData newOption;

    public event EventHandler<ChangeCharacterEventEventArgs> ChangeCharacterEvent;

    public class ChangeCharacterEventEventArgs : EventArgs {
        public int id;
    }

    private void Awake(){
        newOption = new TMPro.TMP_Dropdown.OptionData();
        
        InitializeDropdown();
        backButton.onClick.AddListener(() => { When_BackButtonPressed(); });
    }

    public void ChangeCharacter(){
        character = characterSelecter.value;
        ChangeCharacterEvent?.Invoke(this, new ChangeCharacterEventEventArgs { id = character } );
    }

    private void InitializeDropdown(){
        int characterCount = CharacterData.Characters.Count;

        for (int i = 0; i < characterCount; i ++){
            characterSelecter.options.Add(new TMPro.TMP_Dropdown.OptionData { text = CharacterData.Characters[i].name });
        }
    }

    private void When_BackButtonPressed(){
        ChangeCharacter();
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        
    }






}
