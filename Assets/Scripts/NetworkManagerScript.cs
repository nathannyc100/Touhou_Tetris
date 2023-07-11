using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;   

public class NetworkManagerScript : NetworkBehaviour
{
    
    private BoardSyncManager boardSyncManager;
    private Board board;
    private GameManager gameManager;
    
    public NetworkList<ushort> syncBoard = new NetworkList<ushort>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public event EventHandler<SyncBoardEventArgs> SyncBoard;

    public class SyncBoardEventArgs : EventArgs {
        public List<ushort> boardList;
    }


    public override void OnNetworkSpawn()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.InitializeNetworkScript += When_InitializeNetworkScript;

        base.OnNetworkSpawn();
    }

    private void When_InitializeNetworkScript(object sender, EventArgs e){
        for (int i = 0; i < 200; i ++){
            syncBoard.Add(0);
        }

        if (IsClient && !IsOwner){
            boardSyncManager = FindObjectOfType<BoardSyncManager>();
            boardSyncManager.GetNetworkReference(this);
            syncBoard.OnListChanged += UpdateBoard;

        }

        if (IsOwner){
            board = FindObjectOfType<Board>();
            board.UpdateSyncBoardEvent += When_UpdateSyncBoardEvent;
        }
    }

    private void UpdateBoard(NetworkListEvent<ushort> changeEvent){
        List<ushort> copyList = new List<ushort>();

        foreach (ushort i in syncBoard){
            copyList.Add(i);
        }

        SyncBoard?.Invoke(this, new SyncBoardEventArgs { boardList = copyList } );
    }

    private void When_UpdateSyncBoardEvent(object sender, Board.UpdateSyncBoardEventArgs e){
        foreach (int i in e.syncBoard){
            syncBoard[i] = (ushort)e.syncBoard[i];
        }
    }


}
