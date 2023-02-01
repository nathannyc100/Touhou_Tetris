using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private OptionsMenu optionsMenu;
    [SerializeField]
    private GameObject gameManager;

    public static GameState GameCurrentState;
    [System.NonSerialized]
    public int character;
    [System.NonSerialized]
    public string text;

    public enum GameState {
        StartMenu,
        Tetris,
    }

    

    private void Awake(){
        GameCurrentState = GameState.StartMenu;
        DontDestroyOnLoad(gameManager);
    }

    private void Start(){
        mainMenu.StartGame += When_StartGame;
        optionsMenu.ChangeCharacterEvent += When_ChangeCharacterEvent;

    }

    private void When_StartGame(object sender, EventArgs e){
        if (GameCurrentState == GameState.StartMenu){
            GameCurrentState = GameState.Tetris;
            SceneManager.LoadScene("Tetris");
        }
    }

    private void When_ChangeCharacterEvent(object sender, OptionsMenu.ChangeCharacterEventEventArgs e){
        this.character = e.id;
    }

}
