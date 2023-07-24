using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class NetworkGameManager : NetworkBehaviour
{
    private GameManager gameManager;

    private float startTime;
    private float countdownTime;
    private float countdownDuration = 3f;
    private bool gameIsPlaying = false;

    public NetworkVariable<float> gameTime = new NetworkVariable<float>(0);
    public NetworkVariable<bool> p1Ready = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> p2Ready = new NetworkVariable<bool>(false);

    public event EventHandler MultiplayerStartCountdown;

    public override void OnNetworkSpawn(){
        if (!IsServer){
            gameManager = FindObjectOfType<GameManager>();
            gameManager.GetNetworkReference(this);
        }

        base.OnNetworkSpawn();
    }

    private void Update(){
        if (!IsServer){
            return;
        } 
        
        if (p1Ready.Value && p2Ready.Value){
            StartCountdownClientRPC();
            countdownTime = Time.time;
        }

        if (Time.time - countdownTime >= countdownDuration){
            startTime = Time.time;
            gameIsPlaying = true;
        }

        if (gameIsPlaying){
            gameTime.Value = Time.time - startTime;
        }


    }

    [ClientRpc]
    private void StartCountdownClientRPC(){
        MultiplayerStartCountdown?.Invoke(this, EventArgs.Empty);
    }


}
