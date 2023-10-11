using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectMenu : MonoBehaviour
{
    private CharacterManager characterManager;
    private MainMenu mainMenu;
    private GameManager gameManager;
    private NetworkGameManager networkGameManager;
    private ControlsManager controlsManager;
    
    [SerializeField]
    private GameObject characterSelectScreen;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private TextMeshProUGUI characterName;

    private int selectHold;
    private float lastHoldTime;
    private float holdInterval = 0.3f;

    private void Awake() {
        mainMenu = GetComponent<MainMenu>();
        gameManager = GameManager.Singleton;
        controlsManager = FindObjectOfType<ControlsManager>();
        characterManager = FindObjectOfType<CharacterManager>();
        Debug.LogWarning(controlsManager);
        
        startButton.onClick.AddListener(() => { When_StartButtonPressed(); });

    }

    private void OnEnable() {
        mainMenu.OpenCharacterSelectMenu += When_OpenCharacterSelectMenu;
        gameManager.OpenCharacterSelectMenu += When_OpenCharacterSelectMenu;
        controlsManager.OnKeyPressed += When_OnKeyPressed;
    }

    private void OnDisable() {
        mainMenu.OpenCharacterSelectMenu -= When_OpenCharacterSelectMenu;
        gameManager.OpenCharacterSelectMenu -= When_OpenCharacterSelectMenu;
        controlsManager.OnKeyPressed -= When_OnKeyPressed;
    }

    private void Update() {
        if (selectHold == 0 || Time.time - holdInterval >= lastHoldTime) {
            return;
        }

        if (selectHold == -1) {
            characterManager.CharacterSelect(-1);
        } else if (selectHold == 1) {
            characterManager.CharacterSelect(1);
        }
    }

    private void When_OpenCharacterSelectMenu(object sender, EventArgs e) {
        characterSelectScreen.SetActive(true);
        UpdateCharacterText();
    } 

    private void When_StartButtonPressed() {
        if (GameManager.GameCurrentMode == GameManager.GameType.Singleplayer) {
            gameManager.StartGame();
        } else {
            networkGameManager = NetworkGameManager.Singleton;
            networkGameManager.PlayerReady();
        }

    }

    private void When_OnKeyPressed(object sender, ControlsManager.OnKeyPressedEventArgs e) {
        switch (e.action) {
            case ControlsManager.ActionName.SelectLeft:
                characterManager.CharacterSelect(-1);
                break;
            case ControlsManager.ActionName.SelectRight:
                characterManager.CharacterSelect(1);
                break;
            case ControlsManager.ActionName.SelectLeftHeld:
                selectHold = -1;
                lastHoldTime = Time.time;
                break;
            case ControlsManager.ActionName.SelectRightHeld:
                selectHold = 1;
                lastHoldTime = Time.time;
                break;
            case ControlsManager.ActionName.SelectCancelled:
                selectHold = 0;
                break;
            default:
                break;

        }
    }

    public void UpdateCharacterText() {
        characterName.text = characterManager.currentCharacter.name;
    }




}
