using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    private Board board;
    private Buffs buffs;
    private Skills skills;
    private GameManager gameManager;

    public int health;
    private int damage;
    private int sendDamage;
    private bool sendDamageBool;

    public event EventHandler<DamageDeltEventArgs> DamageDelt;
    public event EventHandler<HealthChangedEventArgs> HealthChanged;
    public event EventHandler RegularAttackStopped;
    public event EventHandler GameOverEvent;

    public class DamageDeltEventArgs : EventArgs {
        public int damage;
    }

    public class HealthChangedEventArgs : EventArgs {
        public int health;
    }

    private void Awake(){
        this.board = FindObjectOfType<Board>();
        this.buffs = GetComponent<Buffs>();
        this.skills = GetComponent<Skills>();
        this.gameManager = GameManager.instance;
    }

    private void OnEnable(){
        board.LineCleared += When_LineCleared_DamageCalc;
        gameManager.ResetGame += When_ResetGame_InitializeValues;
        skills.Heal += When_Heal;
    }

    private void OnDisable(){
        board.LineCleared -= When_LineCleared_DamageCalc;
        gameManager.ResetGame -= When_ResetGame_InitializeValues;
        skills.Heal -= When_Heal;
    }

    private void LateUpdate(){
        
        // Multiple line clear damage sending 
        if (sendDamage != 0){
            if (sendDamageBool == false){
                DamageDelt?.Invoke(this, new DamageDeltEventArgs { damage = sendDamage } );
                sendDamage = 0;
            }
        }

        if (sendDamageBool){
            sendDamageBool = false;
        }
        
    }

    private void When_LineCleared_DamageCalc(object sender, Board.LineClearedEventArgs e){
        if (buffs.totalBuffs.StopRegularAttack){
            RegularAttackStopped?.Invoke(this, EventArgs.Empty);
            return;
        }

        damage = 0;
        float attack = CharacterData.Characters[board.character].attack;
        float[] multiplier = CharacterData.Characters[board.character].multiplier;
        float[] colorBuffs = buffs.totalBuffs.BlockColorDamageBuff;
        float attakBuff = buffs.totalBuffs.SelfDamageMultiplier;

        for (int i = 0; i < 5; i ++){
            damage += Mathf.CeilToInt(attack * e.colorArray[i] * multiplier[i] * colorBuffs[i]);
        }
        
        health -= damage;
        AddDamageToSendLineDamage(damage);
        HealthChanged?.Invoke(this, new HealthChangedEventArgs { health = this.health } );

        if (health <= 0){
            GameOverEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    private void When_ResetGame_InitializeValues(object sender, EventArgs e){
        health = CharacterData.Characters[board.character].health;
        
        HealthChanged?.Invoke(this, new HealthChangedEventArgs { health = this.health } );
    }

    private void When_Heal(object sender, Skills.HealEventArgs e){
        health += e.amount;
        // if (health > CharacterData.Characters[board.character].health){
        //     health = CharacterData.Characters[board.character].health;
        // }
        HealthChanged?.Invoke(this, new HealthChangedEventArgs { health = this.health } );
    }

    private void AddDamageToSendLineDamage(int damage){
        this.sendDamage += damage;
        this.sendDamageBool = true;
    }
}
