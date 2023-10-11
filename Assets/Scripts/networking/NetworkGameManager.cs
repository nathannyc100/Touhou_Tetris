using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class NetworkGameManager : NetworkBehaviour
{
    public static NetworkGameManager Singleton;

    private GameManager gameManager;
    private NetworkTimingManager networkTimingManager;
    private CharacterManager characterManager;

    
     
    private bool p1Ready;
    private bool p2Ready;
    private bool sceneLoaded = false;
    private bool lost = false;

    public NetworkVariable<float> gameTime = new NetworkVariable<float>(0);
    public NetworkVariable<GameManager.GameState> network_gameState = new NetworkVariable<GameManager.GameState>(GameManager.GameState.StartMenu, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    

    public event EventHandler MultiplayerStartCountdown;
    public event EventHandler<SceneLoadCompleteEventArgs> SceneLoadComplete; 
    public event EventHandler<GameOverEventArgs> GameOverEvent;
    public event EventHandler ResetGame;

    public class SceneLoadCompleteEventArgs : EventArgs {
        public string sceneName;
    }

    public class GameOverEventArgs : EventArgs {
        public bool won;
    }

    private void MakeSingleton(){
        if (Singleton != null){
            Destroy(gameObject);
        } else {
            Singleton = this;
        }

    }

    public override void OnNetworkSpawn(){
        MakeSingleton();

        //networkTimingManager = NetworkTimingManager.Singleton;
        gameManager = GameManager.Singleton;
        characterManager = FindObjectOfType<CharacterManager>();
        characterManager.GetNetworkReference(this);

        NetworkManager.Singleton.SceneManager.OnSceneEvent += When_OnSceneEvent;

        network_gameState.OnValueChanged += UpdateGameState;

        if (IsHost){
            Debug.Log("is host");
            if (GameManager.GameCurrentMode == GameManager.GameType.Singleplayer){
                LoadNextScene("Tetris");
            }

            if (GameManager.GameCurrentMode == GameManager.GameType.Multiplayer){
                p1Ready = false;
                p2Ready = false;
            }
        }



        base.OnNetworkSpawn();
    }

    private void Start(){
        networkTimingManager = NetworkTimingManager.Singleton;
    }

    private void Update(){
        if (!IsServer){
            return;
        } 
        
        if (p1Ready && p2Ready && GameManager.GameCurrentMode == GameManager.GameType.Multiplayer && network_gameState.Value == GameManager.GameState.StartMenu){
            network_gameState.Value = GameManager.GameState.CountdownScreen;
            InitializeCharacterManagerClientRPC();
            LoadNextScene("Tetris");
        }


    }

    [ClientRpc]
    private void StartCountdownClientRPC(){
        MultiplayerStartCountdown?.Invoke(this, EventArgs.Empty);
    }

    

    private void When_OnSceneEvent(SceneEvent sceneEvent) {
        if (sceneEvent.SceneEventType == SceneEventType.Load){
            sceneLoaded = false;
        }
        if (sceneEvent.SceneEventType != SceneEventType.LoadEventCompleted || sceneLoaded == true) { 
            return;
        }

        
        Debug.Log("Done loading " + sceneEvent.SceneName);

        SceneLoadComplete?.Invoke(this, new SceneLoadCompleteEventArgs { sceneName = sceneEvent.SceneName });
        gameManager.ReloadGameManagerDependencies();

        if (sceneEvent.SceneName == "Tetris" && IsServer){
            
            RestartGameServerRPC();
            
            
            networkTimingManager.StartCountdown();


            //StartCountdownClientRPC();
            //StartCoroutine("CountdownCorutine");
        }

        sceneLoaded = true;
    }

    private void LoadNextScene(string sceneName){
        Debug.Log("loading next scene");
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void PlayerReady() {
        if (IsHost) {
            p1Ready = true;
        } else {
            Player2ReadyServerRPC();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void Player2ReadyServerRPC(){ 
        p2Ready = true;
    } 
    
    private void UpdateGameState(GameManager.GameState previous, GameManager.GameState current){
        GameManager.GameCurrentState = current;

        if (current == GameManager.GameState.GameOver){
            GameOverEvent?.Invoke(this, new GameOverEventArgs { won = !lost });

        }

        
    }

    public void GameOver(){
        lost = true;

        GameOverServerRPC();
    }

    [ServerRpc]
    private void GameOverServerRPC(){
        network_gameState.Value = GameManager.GameState.GameOver;
    }

    [ServerRpc]
    public void RestartGameServerRPC(){
        //networkTimingManager.PauseGameServerRPC(false);

        network_gameState.Value = GameManager.GameState.CountdownScreen;
        ResetGameClientRPC();
    }

    [ClientRpc]
    private void ResetGameClientRPC() {
        ResetGame?.Invoke(this, EventArgs.Empty);
        lost = false;
    }

    [ClientRpc]
    private void InitializeCharacterManagerClientRPC() {
        characterManager.Initialize();
    }


}
