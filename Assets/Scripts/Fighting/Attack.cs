using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System;
using System.Net;

public class Attack : MonoBehaviour {
    private Board board;
    private Buffs buffs;
    private CharacterManager characterManager;

    private int sendDamage;
    private bool sendDamageBool;

    public event EventHandler<DealDamageEventArgs> DealDamage;
    public event EventHandler RegularAttackStopped;

    public class DealDamageEventArgs : EventArgs {
        public int damage;
    }

    private void Awake(){
        board = FindObjectOfType<Board>();
        buffs = FindObjectOfType<Buffs>();  
        characterManager = FindObjectOfType<CharacterManager>();
    }

    private void OnEnable(){
        board.LineCleared += When_LineCleared;
    }

    private void OnDisable(){
        board.LineCleared -= When_LineCleared;
    }

    private void LateUpdate(){
        
        // Multiple line clear damage sending 
        if (sendDamage != 0){
            if (sendDamageBool == false){
                DealDamage?.Invoke(this, new DealDamageEventArgs { damage = sendDamage } );
                sendDamage = 0;
            }
        }

        if (sendDamageBool){
            sendDamageBool = false;
        }
        
    }

    private void When_LineCleared(object sender, Board.LineClearedEventArgs e){
        if (buffs.totalBuffs.StopRegularAttack){
            RegularAttackStopped?.Invoke(this, EventArgs.Empty);
            return;
        }

        int damage = 0;
        float attack = characterManager.currentCharacter.characterAttack;
        float[] multiplier = characterManager.currentCharacter.colorMultiplier;
        float[] colorBuffs = buffs.totalBuffs.BlockColorDamageBuff;
        float attakBuff = buffs.totalBuffs.SelfDamageMultiplier;

        for (int i = 0; i < 5; i++){
            damage += Mathf.CeilToInt(attack * e.colorArray[i] * multiplier[i] * colorBuffs[i]);
        }

        sendDamage += damage;
        sendDamageBool = true;
    }


}
