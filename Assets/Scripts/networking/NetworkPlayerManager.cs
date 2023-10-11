using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class NetworkPlayerManager : NetworkBehaviour
{

    private BoardSyncManager boardSyncManager;
    private Board board;
    private Health health;
    private Mana mana;
    private GameManager gameManager;
    [SerializeField]
    private GameObject networkGameManagerPrefab;
    [SerializeField]
    private GameObject networkTimingMangerPrefab;
    private NetworkGameManager networkGameManager;
    private NetworkTimingManager networkTimingManager;
    private Attack attack;
    private PlayerUI playerUI;
    private EnemyUI enemyUI;
    private SkillManager skillManager;
    private CharacterManager characterManager;

    private int syncBoardLength = 200;

    public NetworkList<ushort> network_syncBoard;
    public NetworkVariable<ushort> network_syncHold = new NetworkVariable<ushort>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<ushort> network_syncHoldColor = new NetworkVariable<ushort>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<ushort> network_syncCharacter = new NetworkVariable<ushort>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> network_syncHealth = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> network_syncMana = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkList<ushort> network_skillCountdown;
    public NetworkList<ushort> network_skillfBuffDuration;


    public event EventHandler<Server_SyncBoardEventArgs> Server_SyncBoard;
    public event EventHandler<Server_SyncHoldEventArgs> Server_SyncHold;
    public event EventHandler<Server_SyncPlayerEventArgs> Server_SyncPlayer;
    public event EventHandler<Server_DealDamageEventArgs> Server_DealDamage;
    public event EventHandler Server_RefreshDontDestroyOnLoad;
    public event EventHandler<Server_SyncSkillsEventArgs> Server_SyncSkills;
    public event EventHandler<Server_SyncBuffsEventArgs> Server_SyncBuffs;

    public class Server_SyncBoardEventArgs : EventArgs {
        public int[] boardList;
    }

    public class Server_SyncHoldEventArgs : EventArgs {
        public int hold;
        public int color;
    }

    public class Server_SyncPlayerEventArgs : EventArgs {
        public int health;
        public int mana;
    }

    public class Server_DealDamageEventArgs : EventArgs {
        public int damage;
    }

    public class Server_SyncSkillsEventArgs : EventArgs {
        public int[] skillCD;
    }

    public class Server_SyncBuffsEventArgs : EventArgs {
        public int[] buffDuration;
    }

    private void Awake() {
        network_syncBoard = new NetworkList<ushort>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        network_skillCountdown = new NetworkList<ushort>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        network_skillfBuffDuration = new NetworkList<ushort>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    }

    public override void OnNetworkSpawn()
    {
        gameManager = GameManager.Singleton;

        if (IsServer) {
            networkGameManager = FindObjectOfType<NetworkGameManager>();

            if (networkGameManager != null) {
                return;
            }

            networkGameManager = InitializeNetworkPrefab<NetworkGameManager>(networkGameManagerPrefab);

            Debug.Log("network game manager instantiated");

            for (int i = 0; i < 5; i ++) {
                network_skillCountdown.Add(0);
                network_skillfBuffDuration.Add(0);
            }
        }

        if (!IsOwner){
            return;
        }

       

        for (int i = 0; i < 200; i++){
            network_syncBoard.Add(0);
        }
        


        base.OnNetworkSpawn();
    }

    private void Start(){
        networkGameManager = NetworkGameManager.Singleton;
        networkTimingManager = NetworkTimingManager.Singleton;
        characterManager = FindObjectOfType<CharacterManager>();    
        networkGameManager.SceneLoadComplete += When_SceneLoadComplete;
        networkGameManager.ResetGame += When_ResetGame;

        if (IsOwner){
            networkTimingManager.TimeIncrement += When_GameTick;
            characterManager.GetPlayerManagerReference(this)
;        }

        if (!IsOwner) {
            characterManager.GetEnemyNetworkReference(this);
        }
        
    }

    private T InitializeNetworkPrefab<T>(GameObject prefab){
        GameObject gameObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        gameObject.GetComponent<NetworkObject>().Spawn(false);

        return gameObject.GetComponent<T>();
    }

    private void When_SceneLoadComplete(object sender, NetworkGameManager.SceneLoadCompleteEventArgs e){

        if (e.sceneName != "Tetris"){
            return;
        }

        network_syncHealth.OnValueChanged += UpdatePlayerHealth;
        network_syncMana.OnValueChanged += UpdatePlayerMana;

        if (IsClient && !IsOwner){
            boardSyncManager = FindObjectOfType<BoardSyncManager>();
            boardSyncManager.GetNetworkReference(this);
            attack = FindObjectOfType<Attack>();
            attack.DealDamage += When_DealDamage;
            network_syncBoard.OnListChanged += UpdateBoard;
            network_syncHold.OnValueChanged += UpdateHold;
            network_syncHoldColor.OnValueChanged += UpdateHoldColor;
            network_skillCountdown.OnListChanged += UpdateSkillCountdown;
            network_skillfBuffDuration.OnListChanged += UpdateBuffDuration;

            enemyUI = FindObjectOfType<EnemyUI>();
            enemyUI.GetNetworkDependencies(this);

            skillManager = FindObjectOfType<SkillManager>();
            skillManager.GetNetowrkEnemy(this);
            

        }

        if (IsOwner){
            board = FindObjectOfType<Board>();
            board.UpdateSyncBoardEvent += When_UpdateSyncBoardEvent;
            board.UpdateSyncHoldEvent += When_UpdateSyncHoldEvent;

            mana = FindObjectOfType<Mana>();
            mana.ChangeMana += When_ChangeMana;

            playerUI = FindObjectOfType<PlayerUI>();    
            playerUI.GetNetworkDependencies(this);

            skillManager = FindObjectOfType<SkillManager>();
            skillManager.GetNetworkPlayer(this);
            skillManager.SkillTriggered += When_SkillTriggered;

            

            
        }
    }


    private void UpdateBoard(NetworkListEvent<ushort> changeEvent){
        int[] copyList = new int[200];

        for (int i = 0; i < 200; i++)
        {
            copyList[i] = network_syncBoard[i];
            if (copyList[i] != 5){
            }
        }


        Server_SyncBoard?.Invoke(this, new Server_SyncBoardEventArgs { boardList = copyList } );
    }

    private void UpdateHold(ushort previous, ushort current) {
        Server_SyncHold?.Invoke(this, new Server_SyncHoldEventArgs { hold = current, color = -1 });
    }

    private void UpdateHoldColor(ushort previous, ushort current) {
        Server_SyncHold?.Invoke(this, new Server_SyncHoldEventArgs { hold = -1, color = current });
    }

    private void UpdatePlayerHealth(int previous, int current){
        Server_SyncPlayer?.Invoke(this, new Server_SyncPlayerEventArgs { health = current, mana = network_syncMana.Value});
        if (current <= 0){
            networkGameManager.GameOver();
        }
    }

    private void UpdatePlayerMana(int previous, int current){
        Server_SyncPlayer?.Invoke(this, new Server_SyncPlayerEventArgs { health = network_syncHealth.Value, mana = current });
    }

    private void UpdateSkillCountdown(NetworkListEvent<ushort> changeEvent) {
        int[] copyList = new int[5];

        for (int i = 0; i < 5; i++) {
            copyList[i] = network_skillCountdown[i];
        }

        Server_SyncSkills?.Invoke(this, new Server_SyncSkillsEventArgs { skillCD = copyList });
    }

    private void UpdateBuffDuration(NetworkListEvent<ushort> changeEvent) {
        int[] copyList = new int[5];

        for (int i = 0; i < 5; i++) {
            copyList[i] = network_skillfBuffDuration[i];
        }

        Server_SyncBuffs?.Invoke(this, new Server_SyncBuffsEventArgs { buffDuration = copyList });
    }

    private void When_UpdateSyncBoardEvent(object sender, Board.UpdateSyncBoardEventArgs e){
        int i;
        for (i = 0; i < syncBoardLength; i ++){
            network_syncBoard[i] = (ushort)e.syncBoard[i];
        }
    }

    private void When_UpdateSyncHoldEvent(object sender, Board.UpdateSyncHoldEventArgs e){
        network_syncHold.Value = (ushort)e.hold;
        network_syncHoldColor.Value = (ushort)e.color;
    }

    private void When_ChangeMana(object sender, Mana.ChangeManaEventArgs e){
        ChangeManaServerRPC(e.amount);
    }

    private void When_DealDamage(object sender, Attack.DealDamageEventArgs e){
        DealDamageServerRPC(e.damage);
    }

    [ServerRpc(RequireOwnership = false)]
    public void DealDamageServerRPC(int damage) {
        network_syncHealth.Value -= damage;

    }

    [ServerRpc(RequireOwnership = false)]
    public void ChangeManaServerRPC(int mana) {
        network_syncMana.Value += mana;
    }

    [ServerRpc(RequireOwnership = false)]
    public void HealServerRPC(int amount) {
        network_syncHealth.Value += amount;
    }


    private void When_ResetGame(object sender, EventArgs e){
        if (!IsServer){ 
            return;
        }

        network_syncHealth.Value = characterManager.currentCharacter.characterHealth;
        network_syncMana.Value = 0;

    }

    private void When_SkillTriggered(object sender, SkillManager.SkillTriggeredEventArgs e) {
        network_skillCountdown[e.skill] = (ushort)e.time;
    }

    private void When_GameTick(object sender, NetworkTimingManager.TimeIncrementEventArgs e) {
        for (int i = 0; i < 5; i++) {
            if (network_skillCountdown[i] > 0) {
                network_skillCountdown[i] --;
            }
        }
    }

    public void UpdateCharacter(int index) {
        network_syncCharacter.Value = (ushort)index;
    }


}
