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
    private bool p1Ready;
    private bool p2Ready;

    public NetworkVariable<float> gameTime = new NetworkVariable<float>(0);
    


    public event EventHandler MultiplayerStartCountdown;
    public event EventHandler<SceneLoadCompleteEventArgs> SceneLoadComplete;

    public class SceneLoadCompleteEventArgs : EventArgs {
        public string sceneName;
    }

    public override void OnNetworkSpawn(){
        if (IsHost || IsClient){
            gameManager = FindObjectOfType<GameManager>();
            gameManager.GetNetworkReference(this);
            NetworkManager.Singleton.SceneManager.OnSceneEvent += When_OnSceneEvent;

            
        }

        if (IsHost){
            //gameManager.LoadNextSceneEvent += When_LoadNextSceneEvent;
            Debug.Log("is host");
            if (GameManager.GameCurrentMode == GameManager.GameType.Singleplayer){
                LoadNextScene("Tetris");
            }

            if (GameManager.GameCurrentMode == GameManager.GameType.Multiplayer){
                p1Ready = true;
                p2Ready = false;
            }
        }



        base.OnNetworkSpawn();
    }

    private void Update(){
        if (!IsServer){
            return;
        } 
        
        if (p1Ready && p2Ready && GameManager.GameCurrentMode == GameManager.GameType.Multiplayer && GameManager.GameCurrentState == GameManager.GameState.StartMenu){
            GameManager.GameCurrentState = GameManager.GameState.Tetris;
            LoadNextScene("Tetris");
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

    private void When_OnSceneEvent(SceneEvent sceneEvent) {
        if (sceneEvent.SceneEventType != SceneEventType.LoadEventCompleted) { 
            return;
        }

        SceneLoadComplete?.Invoke(this, new SceneLoadCompleteEventArgs { sceneName = sceneEvent.SceneName });

        if (sceneEvent.SceneName == "Tetris"){
            StartCountdownClientRPC();
            countdownTime = Time.time;
        }
    }

    private void LoadNextScene(string sceneName){
        Debug.Log("loading next scene");
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    [ServerRpc(RequireOwnership = false)]
    public void Player2ReadyServerRPC(){ 
        p2Ready = true;
    }


}
