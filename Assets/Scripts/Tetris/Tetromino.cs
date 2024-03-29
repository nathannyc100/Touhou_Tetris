using UnityEngine.Tilemaps;
using UnityEngine;

public enum Tetromino {
    I,
    O,
    T,
    J,
    L,
    S,
    Z
}

[System.Serializable]
public struct TetrominoData {
    public Tetromino tetromino;
    public Tile tile;
    public Vector2Int[] cells;
    public Vector2Int[,] wallKicks;

    public void Initialize() {
        this.cells = Data.Cells[this.tetromino];
        this.wallKicks = Data.WallKicks[this.tetromino];
    }
}

 