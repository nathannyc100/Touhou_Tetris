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
    private Vector3Int tileOffset = new Vector3Int(-10, -20, 0);
    [SerializeField]
    private TileBase[] tileColor;

    private void Awake(){
        board = FindObjectOfType<Board>();

        this.bounds = board.Bounds;
        this.boardSize = board.boardSize;

        tilemap = GetComponentInChildren<Tilemap>();
    }

    public void GetNetworkReference(NetworkPlayerManager script){
        networkPlayerManager = script;

        networkPlayerManager.SyncBoard += When_SyncBoard;
    }

    private void When_SyncBoard(object sender, NetworkPlayerManager.SyncBoardEventArgs e){
        int i, j;

        for (i = 0; i < boardSize.x; i ++){
            for (j = 0; j < boardSize.y; j ++){
                Vector3Int tilePosition = new Vector3Int(bounds.xMax + i, bounds.yMax + j, 0);
                tilePosition += tileOffset;
                if (e.boardList[j * 10 + i] == 5){
                    tilemap.SetTile(tilePosition, null);
                } else {
                    tilemap.SetTile(tilePosition, tileColor[e.boardList[j * 10 + i]]);
                }
            }
        }
    }
}
