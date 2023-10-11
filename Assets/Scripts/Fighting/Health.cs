using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    private Board board;
    private Buffs buffs;
    private GameManager gameManager;
    private NetworkPlayerManager networkPlayerManager;
    private CharacterManager characterManager;

    private int health = 100;

    public event EventHandler<DamageDeltEventArgs> DamageDelt;
    public event EventHandler<HealthChangedEventArgs> HealthChanged;
    public event EventHandler RegularAttackStopped;

    public class DamageDeltEventArgs : EventArgs {
        public int damage;
    }

    public class HealthChangedEventArgs : EventArgs {
        public int health;
    }

    private void Awake(){
        board = FindObjectOfType<Board>();
        buffs = GetComponent<Buffs>();
        gameManager = GameManager.Singleton;
        characterManager = FindObjectOfType<CharacterManager>();
    }

    private void OnEnable(){
        gameManager.ResetGame += When_ResetGame_InitializeValues;
    }

    private void OnDisable(){
        gameManager.ResetGame -= When_ResetGame_InitializeValues;
    }

    private void LateUpdate(){
        if (GameManager.GameCurrentState != GameManager.GameState.Tetris){
            return;
        }

        if (health <= 0){
            Debug.LogWarning(health + " health game over");
            //gameManager.GameOver();
        }
       
        
    }

    private void When_ResetGame_InitializeValues(object sender, EventArgs e){
        health = characterManager.currentCharacter.characterHealth;
        Debug.Log("health = " + health);
        HealthChanged?.Invoke(this, new HealthChangedEventArgs { health = this.health } );
    }



}
