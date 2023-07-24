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
    private GameObject networkGameManager;
    private int syncBoardLength = 200;

    public NetworkList<ushort> syncBoard = new NetworkList<ushort>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<ushort> syncHealth = new NetworkVariable<ushort>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<ushort> syncMana = new NetworkVariable<ushort>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    public event EventHandler<SyncBoardEventArgs> SyncBoard;
    public event EventHandler<SyncPlayerEventArgs> SyncPlayer;
    public event EventHandler RefreshDontDestroyOnLoad;

    public class SyncBoardEventArgs : EventArgs {
        public List<ushort> boardList;
    }

    public class SyncPlayerEventArgs : EventArgs{
        public int health;
        public int mana;
    }

    public override void OnNetworkSpawn()
    {
        DontDestroyOnLoad(this.gameObject);
        gameManager = FindObjectOfType<GameManager>();
        gameManager.InitializeNetworkScript += When_InitializeNetworkScript;

        //syncBoard = new NetworkList<ushort>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        if (IsServer){
            NetworkGameManager testFirstManager = FindObjectOfType<NetworkGameManager>();

            if (testFirstManager != null){
                networkGameManager = testFirstManager.gameObject;
                //DontDestroyOnLoad(networkGameManager);
                return;
            }

            networkGameManager = Instantiate(networkGameManagerPrefab, Vector3.zero, Quaternion.identity);
            //DontDestroyOnLoad(networkGameManager);
            networkGameManager.GetComponent<NetworkObject>().Spawn(false);
            

            Debug.Log("network game manager instantiated");
        }

        base.OnNetworkSpawn();
    }

    private void When_InitializeNetworkScript(object sender, EventArgs e){

        
        

        if (IsClient && !IsOwner){
            boardSyncManager = FindObjectOfType<BoardSyncManager>();
            boardSyncManager.GetNetworkReference(this);
            syncBoard.OnListChanged += UpdateBoard;
            syncHealth.OnValueChanged += UpdatePlayerHealth;
            

        }

        if (IsOwner){
            board = FindObjectOfType<Board>();
            board.UpdateSyncBoardEvent += When_UpdateSyncBoardEvent;

            health = FindObjectOfType<Health>();
            health.HealthChanged += When_HealthChanged;

            mana = FindObjectOfType<Mana>();
            mana.ManaChanged += When_ManaChanged;

            for (int i = 0; i < 200; i++)
            {
                syncBoard.Add(0);
            }
        }
    }

    private void UpdateBoard(NetworkListEvent<ushort> changeEvent){
        List<ushort> copyList = new List<ushort>();

        foreach (ushort i in syncBoard){
            copyList.Add(i);
        }

        SyncBoard?.Invoke(this, new SyncBoardEventArgs { boardList = copyList } );
    }

    private void UpdatePlayerHealth(ushort previous, ushort current){
        SyncPlayer?.Invoke(this, new SyncPlayerEventArgs { health = current });
    }

    private void UpdatePlayerMana(ushort previous, ushort current){
        SyncPlayer?.Invoke(this, new SyncPlayerEventArgs { mana = current });
    }

    private void When_UpdateSyncBoardEvent(object sender, Board.UpdateSyncBoardEventArgs e){
        int i;
        for (i = 0; i < syncBoardLength; i ++){
            syncBoard[i] = (ushort)e.syncBoard[i];
        }
    }

    private void When_HealthChanged(object sender, Health.HealthChangedEventArgs e){
        syncHealth.Value = (ushort)e.health;
    }

    private void When_ManaChanged(object sender, Mana.ManaChangedEventArgs e){
        syncMana.Value = (ushort)e.mana;
    }


}
