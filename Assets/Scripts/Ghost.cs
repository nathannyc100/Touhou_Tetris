using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour {
    public Tile tile;

    public Board board;
    public Piece trackingPiece;

    public Tilemap tilemap;
    public Vector3Int[] cells;
    public Vector3Int position;

    private void Awake(){
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.board = FindObjectOfType<Board>();
        this.trackingPiece = FindObjectOfType<Piece>();
        this.cells = new Vector3Int[4];
    }

    private void LateUpdate(){
        Clear();
        Copy();
        Drop();
        Set();
    }

    public void Clear(){
        this.tilemap.ClearAllTiles();
    } 

    private void Copy(){
        for (int i = 0; i < this.cells.Length; i++){
            this.cells[i] = this.trackingPiece.cells[i];
        }
    }

    private void Drop(){
        Vector3Int position = this.trackingPiece.position;

        int current = position.y; 
        int bottom = -this.board.boardSize.y / 2 - 2;

        this.board.Clear(this.trackingPiece);

        for (int row = current; row >= bottom; row--){
            position.y = row;

            if (this.board.IsValidPosition(this.trackingPiece, position, board.activePieceData.orient)){
                this.position = position;
            } else {
                break;
            }
        }

        this.board.Set(this.trackingPiece);
    }

    private void Set(){
        for (int i = 0; i < board.pieceSize; i++){
            Vector3Int tilePosition = (Vector3Int)Data.originalOrient[(board.activePieceData.type * 4) + board.activePieceData.orient, i] + this.position;      //piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, this.tile);
        }
    }
}
