using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSyncManager : MonoBehaviour
{
    private NetworkPlayerManager networkPlayerManager;

    public void GetNetworkReference(NetworkPlayerManager script){
        networkPlayerManager = script;

        networkPlayerManager.SyncBoard += When_SyncBoard;
    }

    private void When_SyncBoard(object sender, NetworkPlayerManager.SyncBoardEventArgs e){

    }
}
