using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;   

public class NetworkManagerScript : NetworkBehaviour
{
    private GameManager gameManager;
    private Health health;
    private Mana mana;

    public int clientID;
    public int enemyID;
    private List<ClientRpcParams> clientRPCParams = new List<ClientRpcParams>();

    private NetworkList<ulong> ClientIDList;
    private NetworkList<int> PlayerCharacter;
    private NetworkList<int> PlayerHealth;
    private NetworkList<int> PlayerMana;
    private NetworkList<int> HoldState;

    public event EventHandler<EnemyCharacterChangedEventArgs> EnemyCharacterChanged;
    public event EventHandler<EnemyHealthChangedEventArgs> EnemyHealthChanged;
    public event EventHandler<EnemyManaChangedEventArgs> EnemyManaChanged;
    public event EventHandler<EnemyHoldChangedEventArgs> EnemyHoldChanged;

    public class EnemyCharacterChangedEventArgs : EventArgs {
        public int id;
    }

    public class EnemyHealthChangedEventArgs : EventArgs {
        public int health;
    }

    public class EnemyManaChangedEventArgs : EventArgs {
        public int mana;
    }

    public class EnemyHoldChangedEventArgs : EventArgs {
        public int hold;
    }

    public void Awake(){
        this.gameManager = GameManager.instance;
        this.health = FindObjectOfType<Health>();
        this.mana = FindObjectOfType<Mana>();

        //health.HealthChanged += When_SelfHealthChanged;
        //mana.ManaChanged += When_SelfManaChanged;

        this.ClientIDList = new NetworkList<ulong>(default, NetworkVariableReadPermission.Owner, NetworkVariableWritePermission.Server);
        this.PlayerCharacter = new NetworkList<int>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        this.PlayerHealth = new NetworkList<int>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        this.PlayerMana = new NetworkList<int>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        this.HoldState = new NetworkList<int>(default, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    }


    public override void OnNetworkSpawn(){

        if (!IsServer){
            AddClientIDServerRPC();
            if (clientID == 0){
                enemyID = 1;
            } else {
                enemyID = 0;
            }

            gameManager.ResetGame += When_ResetGame;

            PlayerCharacter.OnListChanged  += When_PlayerCharacterChanged;
            PlayerHealth.OnListChanged += When_PlayerHealthChanged;
            PlayerMana.OnListChanged += When_PlayerManaChanged;
            HoldState.OnListChanged += When_HoldStateChanged;

        }
    }

    private void Update(){
        
    }
    
    [ServerRpc]
    public void AddClientIDServerRPC(ServerRpcParams serverRpcParams = default){
        int clientIndex;
        ulong id = serverRpcParams.Receive.SenderClientId;
        ClientIDList.Add(id);
        clientIndex = ClientIDList.IndexOf(id);

        clientRPCParams.Add(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new ulong[] {id} } });
        ChangeClientIDClientRPC(clientIndex, clientRPCParams[clientIndex]);
    }

    [ServerRpc]
    public void ChangePlayerHealthServerRPC(int id, int amount, ServerRpcParams serverRpcParams = default){
        if (!CheckIfCorrectID(id, serverRpcParams)){
            return;
        }

        this.PlayerHealth[id] = amount;
    }

    [ServerRpc]
    public void ChangePlayerManaServerRPC(int id, int amount, ServerRpcParams serverRpcParams = default){
        if (!CheckIfCorrectID(id, serverRpcParams)){
            return;
        }

        this.PlayerMana[id] = amount;
    }

    [ClientRpc]
    public void ChangeClientIDClientRPC(int id, ClientRpcParams clientRpcParams = default){
        clientID = id;
        if (id == 0){
            enemyID = 1;
        } else {
            enemyID = 0;
        }
    }

    
    private void When_PlayerCharacterChanged(NetworkListEvent<int> change){
        if (IsServer){
            return;
        }

        EnemyCharacterChanged?.Invoke(this, new EnemyCharacterChangedEventArgs { id = PlayerCharacter[enemyID] });
        
        Debug.Log("Enemy character changed " + PlayerCharacter[enemyID]);
    }

    private void When_PlayerHealthChanged(NetworkListEvent<int> change){
        if (IsServer){
            return;
        }

        EnemyHealthChanged?.Invoke(this, new EnemyHealthChangedEventArgs { health = PlayerHealth[enemyID] });

        Debug.Log("Enemy health changed " + PlayerHealth[enemyID]);
        Debug.Log("Self health changed " + PlayerHealth[clientID]);
    }

    private void When_PlayerManaChanged(NetworkListEvent<int> change){
        if (IsServer){
            return;
        }

        EnemyManaChanged?.Invoke(this, new EnemyManaChangedEventArgs { mana = PlayerMana[enemyID] });

        Debug.Log("Enemy mana changed " + PlayerMana[enemyID]);
    }

    private void When_HoldStateChanged(NetworkListEvent<int> change){
        if (IsServer){
            return;
        }

        EnemyHoldChanged?.Invoke(this, new EnemyHoldChangedEventArgs { hold = HoldState[enemyID] });

        Debug.Log("Enemy hold changed " + HoldState[enemyID]);
    }

    private void When_ResetGame(object sender, EventArgs e){
        EnemyCharacterChanged?.Invoke(this, new EnemyCharacterChangedEventArgs { id = PlayerCharacter[enemyID] });
        EnemyHealthChanged?.Invoke(this, new EnemyHealthChangedEventArgs { health = PlayerHealth[enemyID] });
        EnemyManaChanged?.Invoke(this, new EnemyManaChangedEventArgs { mana = PlayerMana[enemyID] });
        EnemyHoldChanged?.Invoke(this, new EnemyHoldChangedEventArgs { hold = HoldState[enemyID] });
    }

    private void When_SelfHealthChanged(object sender, Health.HealthChangedEventArgs e){
        ChangePlayerManaServerRPC(clientID, e.health);
    } 

    private void When_SelfManaChanged(object sendr, Mana.ManaChangedEventArgs e){

    }

    private bool CheckIfCorrectID(int id, ServerRpcParams serverRpcParams){
        ulong clientIDName = serverRpcParams.Receive.SenderClientId;
        if (clientIDName != ClientIDList[id]){
            return false;
        }

        return true;
    }

    


}
