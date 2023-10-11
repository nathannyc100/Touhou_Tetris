using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardSyncManager : MonoBehaviour
{
    private NetworkPlayerManager networkPlayerManager;
    private Board board;

    private Tilemap tilemap;

    private RectInt bounds;
    private Vector2Int boardSize;
    private Vector3Int holdPosition = new Vector3Int(7, 5);
    [SerializeField]
    private TileBase[] tileColor;
    private Data.PieceData holdPieceData = new Data.PieceData();
    private Vector3Int[] holdPiece = new Vector3Int[4];

    private void Awake(){
        board = FindObjectOfType<Board>();

        bounds = board.Bounds;
        boardSize = board.boardSize;

        tilemap = GetComponentInChildren<Tilemap>();

        holdPieceData.color = 0;

    }

    public void GetNetworkReference(NetworkPlayerManager script){
        networkPlayerManager = script;

        networkPlayerManager.Server_SyncBoard += When_SyncBoard;
        networkPlayerManager.Server_SyncHold += When_SyncHold;
        Debug.Log("Got network reference");
    }

    private void When_SyncBoard(object sender, NetworkPlayerManager.Server_SyncBoardEventArgs e){
        int i, j;

        for (i = 0; i < boardSize.x; i ++){
            for (j = 0; j < boardSize.y; j ++){
                Vector3Int tilePosition = new Vector3Int(bounds.xMin + i, bounds.yMin + j, 0);
                if (e.boardList[j * 10 + i] == 5){
                    tilemap.SetTile(tilePosition, null);
                } else {
                    tilemap.SetTile(tilePosition, tileColor[e.boardList[j * 10 + i]]);
                }
            }
        }
    }

    private void When_SyncHold(object sender, NetworkPlayerManager.Server_SyncHoldEventArgs e) { 
        if (e.hold != -1){
            holdPieceData.type = e.hold;
        }

        if (e.color != -1){ 
            holdPieceData.color = e.color;
        }

        for (int i = 0; i < 4; i++){
            holdPiece[i] = new Vector3Int(Data.originalOrient[holdPieceData.type * 4, i].x, Data.originalOrient[holdPieceData.type * 4, i].y, 0);
        }

        ClearHold();
        SetHold(holdPiece);
    }

    private void SetHold(Vector3Int[] holdPiece){
        for (int i = 0; i < holdPiece.Length; i++){
            Vector3Int holdGrid = holdPiece[i] + holdPosition;
            tilemap.SetTile(holdGrid, tileColor[holdPieceData.color]);
        }

        
    }

    private void ClearHold(){
        for (int i = -2; i < 4; i++){
            for (int j = -2; j < 3; j ++){
                Vector3Int holdGrid = holdPosition + new Vector3Int(i, j, 0);
                this.tilemap.SetTile(holdGrid, null);
            }
        }
    }



}
