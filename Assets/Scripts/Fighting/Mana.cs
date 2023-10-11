using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mana : MonoBehaviour
{
    private Piece piece;
    private Board board;
    private Buffs buffs;
    private GameManager gameManager;

    public int manaCount;
    private bool infiniteMana = false;       //used for testing, custom games

    public event EventHandler<ChangeManaEventArgs> ChangeMana;

    public class ChangeManaEventArgs : EventArgs {
        public int amount;
    }

    private void Awake(){
        this.piece = FindObjectOfType<Piece>();
        this.board = FindObjectOfType<Board>();
        this.buffs = GetComponent<Buffs>();
        this.gameManager = GameManager.Singleton;
    }

    private void OnEnable(){
        board.LineCleared += When_LineCleared_IncrementMana;
    }

    private void OnDisable(){
        board.LineCleared -= When_LineCleared_IncrementMana;  
    }

    private void When_LineCleared_IncrementMana(object sender, EventArgs e){
        int changeAmount = Mathf.CeilToInt(this.buffs.totalBuffs.ManaRegen);

        ChangeManaFunction(changeAmount);
    }

    
    public void ChangeManaFunction(int amount) {
        ChangeMana?.Invoke(this, new ChangeManaEventArgs { amount = amount });
    }






}
