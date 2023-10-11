using UnityEngine;
using Unity.Netcode;
using System;
using System.Collections;

public class NetworkTimingManager : NetworkBehaviour {
    public static NetworkTimingManager Singleton;

    private NetworkGameManager networkGameManager;
    private CountdownScreen countdownScreen;
    private GameManager gameManager;
    private PauseMenu pauseMenu;    

    public NetworkVariable<float> network_gameTime = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> network_gameIsPaused = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> network_feverTime = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> network_finalSkill = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private int countdownTimer = 3;
    private int lastGameTime;

    public event EventHandler<TimeIncrementEventArgs> TimeIncrement;
    
    public class TimeIncrementEventArgs : EventArgs {
        public float time;
    }


    public override void OnNetworkSpawn(){
        MakeSingleton();
        
        gameManager = GameManager.Singleton;

        network_gameIsPaused.OnValueChanged += UpdatePause;
        
        base.OnNetworkSpawn();
    }

    private void MakeSingleton(){
        if (Singleton != null){
            Destroy(gameObject);
        } else {
            Singleton = this;
        }
    }

    private void Start(){
        networkGameManager = NetworkGameManager.Singleton;

        networkGameManager.SceneLoadComplete += When_SceneLoadComplete;
    }

    private void Update(){
        if (!IsServer){
            return;
        }

        if (!network_gameIsPaused.Value){
            network_gameTime.Value += Time.deltaTime;
        }

        if (lastGameTime - network_gameTime.Value > 1){
            GameTickClientRPC(Mathf.FloorToInt(network_gameTime.Value));
        }
    }
    
    private void When_SceneLoadComplete(object sender, NetworkGameManager.SceneLoadCompleteEventArgs e){
        if (e.sceneName != "Tetris"){
            return;
        }

        countdownScreen = FindObjectOfType<CountdownScreen>();
        pauseMenu = FindObjectOfType<PauseMenu>();  
    }

    public void StartCountdown(){
        if (!IsServer){
            return;
        }

        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine(){
        while (countdownTimer > 0){
            CountdownTickClientRPC(countdownTimer);
            countdownTimer--;
            yield return new WaitForSecondsRealtime(1);
        }

        network_gameIsPaused.Value = false;
        network_gameTime.Value = 0;
        lastGameTime = 0;
        networkGameManager.network_gameState.Value = GameManager.GameState.Tetris;
        StartGameClientRPC();
        yield return null;
    }

    [ClientRpc]
    private void CountdownTickClientRPC(int countDown){
        countdownScreen.ChangeCountdownText(countDown.ToString());
    }

    [ClientRpc]
    private void GameTickClientRPC(int gameTime){
        TimeIncrement.Invoke(this, new TimeIncrementEventArgs { time = gameTime });
    }

    [ClientRpc]
    private void StartGameClientRPC(){
        GameManager.GameCurrentState = GameManager.GameState.Tetris;
        countdownScreen.DeactivateCountdownScreen();
    }

    [ServerRpc]
    public void PauseGameServerRPC(bool pause){
        if (pause || GameManager.GameCurrentMode == GameManager.GameType.Multiplayer) {
            return;
        }

        network_gameIsPaused.Value = pause;
    }

    private void UpdatePause(bool previous, bool current) {
        if (current) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }

        pauseMenu.TogglePauseMenu(current);

    }

    

}
