using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Board board;
    [SerializeField]
    private Buffs buffs;
    [SerializeField]
    private Skills skills;

    public int health;
    private int damage;

    public event EventHandler<DamageDeltEventArgs> DamageDelt;
    public event EventHandler<HealthChangedEventArgs> HealthChanged;

    public class DamageDeltEventArgs : EventArgs {
        public int damage;
    }

    public class HealthChangedEventArgs : EventArgs {
        public int health;
    }

    private void OnEnable(){
        board.LineCleared += When_LineCleared_DamageCalc;
        board.ResetGame += When_ResetGame_InitializeValues;
        skills.Heal += When_Heal;
    }

    private void When_LineCleared_DamageCalc(object sender, Board.LineClearedEventArgs e){
        damage = 0;
        float attack = CharacterData.Characters[board.character].attack;
        float[] multiplier = CharacterData.Characters[board.character].multiplier;
        float[] colorBuffs = buffs.totalBuffs.BlockColorDamageBuff;
        float attakBuff = buffs.totalBuffs.SelfDamageMultiplier;

        for (int i = 0; i < 5; i ++){
            damage += Mathf.CeilToInt(attack * e.colorArray[i] * multiplier[i] * colorBuffs[i]);
        }
        
        health -= damage;

        DamageDelt?.Invoke(this, new DamageDeltEventArgs { damage = this.damage } );
        HealthChanged?.Invoke(this, new HealthChangedEventArgs { health = this.health } );
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
}
