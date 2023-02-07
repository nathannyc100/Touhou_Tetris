using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mana : MonoBehaviour
{
    [SerializeField]
    private Piece piece;
    [SerializeField]
    private Board board;
    [SerializeField]
    private Buffs buffs;
    [SerializeField]
    private Timing timing;
    [SerializeField]
    private Skills skills;
    private GameManager gameManager;

    public int manaCount;
    private bool infiniteMana = true;       //used for testing, custom games

    public event EventHandler<ManaChangedEventArgs> ManaChanged;

    public class ManaChangedEventArgs : EventArgs {
        public int mana;
    }

    private void OnEnable(){
        this.gameManager = GameManager.instance;
        board.LineCleared += When_LineCleared_IncrementMana;
        gameManager.ResetGame += When_ResetGame_InitializeMana;
        skills.DecreaseMana += When_DecreaseMana;
    }

    private void When_LineCleared_IncrementMana(object sender, EventArgs e){
        this.manaCount += Mathf.CeilToInt(this.buffs.totalBuffs.ManaRegen);

        ManaChangedFunc();
    }

    private void When_ResetGame_InitializeMana(object sender, EventArgs e){
        if (infiniteMana == true){
            this.manaCount = 999;
        } else {
            this.manaCount = 0;
        }

        ManaChangedFunc();
    }


    private void When_DecreaseMana(object sender, Skills.DecreaseManaEventArgs e){
        manaCount -= e.amount;
        ManaChangedFunc();
    }

    private void ManaChangedFunc(){
        if (infiniteMana == true){
            this.manaCount = 999;
        }
        ManaChanged?.Invoke(this, new ManaChangedEventArgs { mana = this.manaCount } );
    }




}
