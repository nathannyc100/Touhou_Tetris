using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DependencyManager : MonoBehaviour
{
    public static DependencyManager instance;

    // Dependencies 

    public Board board;
    public Buffs buffs;
    public CharacterController characterController;
    public ControlsManager controlsManager;
    public CountdownManager countdownManager;
    public DamagePopup damagePopup;
    public GameAssets gameAssets;
    public Ghost ghost;
    public Health health;
    public MainMenu mainMenu;
    public Mana mana;
    public OptionsMenu optionsMenu;
    public PauseMenu pauseMenu;
    public Piece piece;
    public Skills skills;
    public Text text;
    public Timing timing;
    public Visuals visuals;

    public event EventHandler DependenciesChanged;

    private void Awake(){
        MakeSingleton();
        ReloadDependency();
    }

    private void MakeSingleton(){
        if (instance != null){
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ReloadDependency(){
        this.board = FindObjectOfType<Board>();
        this.buffs = FindObjectOfType<Buffs>();
        this.characterController = FindObjectOfType<CharacterController>();
        this.controlsManager = FindObjectOfType<ControlsManager>();
        this.countdownManager  = FindObjectOfType<CountdownManager>();
        this.damagePopup = FindObjectOfType<DamagePopup>();
        this.gameAssets = FindObjectOfType<GameAssets>();
        this.ghost = FindObjectOfType<Ghost>();
        this.health = FindObjectOfType<Health>();
        this.mainMenu = FindObjectOfType<MainMenu>();
        this.mana = FindObjectOfType<Mana>();
        this.optionsMenu = FindObjectOfType<OptionsMenu>();
        this.pauseMenu = FindObjectOfType<PauseMenu>();
        this.piece = FindObjectOfType<Piece>();
        this.skills = FindObjectOfType<Skills>();
        this.text = FindObjectOfType<Text>();
        this.timing = FindObjectOfType<Timing>();
        this.visuals = FindObjectOfType<Visuals>();
        DependenciesChanged?.Invoke(this, EventArgs.Empty);
    }
}
