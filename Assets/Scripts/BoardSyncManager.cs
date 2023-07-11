using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSyncManager : MonoBehaviour
{
    private NetworkManagerScript networkManagerScript;

    public void GetNetworkReference(NetworkManagerScript script){
        networkManagerScript = script;

        networkManagerScript.SyncBoard += When_SyncBoard;
    }

    private void When_SyncBoard(object sender, NetworkManagerScript.SyncBoardEventArgs e){
        
    }
}
