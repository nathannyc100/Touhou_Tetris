using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class Board : MonoBehaviour {

    private Buffs buffs;
    private GameManager gameManager;
    private Skills skills;
    private Piece activePiece;

    public Tilemap tilemap;
    
    
    public Vector3Int[] holdPiece;
    public TileBase[] tileColor;
    public TetrominoData[] tetrominos;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 24);
    public int color;
    public int[] colorArray; // red blue green yellow purple
    public int character;
    public TetrominoData holdTetromino;
    public bool holdStart = true;
    public bool holdOnce = true;
    public Data.PieceData activePieceData = new Data.PieceData();
    public Data.PieceData holdPieceData = new Data.PieceData();
    public int skillPoints;
    public int pieceSize = 4;
    public int[] typeArray = new int[7] {0, 1, 2, 3, 4, 5, 6};
    public int[] tempTypeArray = new int[7] {0, 1, 2, 3, 4, 5, 6};
    public int randomInt = 6;
    private int temp;

    public Controls controls;

    public event EventHandler<LineClearedEventArgs> LineCleared;
    public event EventHandler ResetGame;
    
    public class LineClearedEventArgs : EventArgs {
        public int[] colorArray;
    }
    
    public Vector3Int holdPosition = new Vector3Int(-10, 5);

    


    public RectInt Bounds {
        get {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake() {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        this.gameManager = Data.GetGameManager();

        

        for (int i = 0; i < this.tetrominos.Length; i ++){
            this.tetrominos[i].Initialize();
        }

        this.character = gameManager.character;
    }

    private void OnEnable(){
        this.buffs = DependencyManager.instance.buffs;
        this.gameManager = GameManager.instance;
        this.skills = DependencyManager.instance.skills;
        this.activePiece = DependencyManager.instance.piece;

        buffs.BuffDisappeared += When_BuffDisappeared_LineClear;
    }

    public void Start(){
        ResetGame?.Invoke(this, EventArgs.Empty);

        SpawnPiece();
    }

    public void CopyArray(int[] targetArray, int[] destinationArray, int arrayLen){
        for(int i = 0; i < arrayLen; i ++){
            destinationArray[i] = targetArray[i];
        }
    }

    public void SwapArray(int[] array1, int[] array2, int arrayLen){
        int temp;
        for (int i = 0; i < arrayLen; i ++){
            temp = array1[i];
            array1[i] = array2[i];
            array2[i] = temp;
        }
    }

    public void RemoveInt(int[] array, int num, int arrayLen){
        for (int i = num; i < arrayLen; i ++){
            array[i] = array[i + 1];
        }
    }

    public int RandomPiece(){
        int index = UnityEngine.Random.Range(0, randomInt);
        int num = tempTypeArray[index];
        RemoveInt(tempTypeArray, index, randomInt);

        randomInt --;
        if (randomInt < 0){
            randomInt = 6;
            CopyArray(typeArray, tempTypeArray, 7);
        }
        
        return num;
    }

    public void SpawnPiece(){
        this.activePieceData.type = RandomPiece();
        TetrominoData data = this.tetrominos[this.activePieceData.type];
        this.activePieceData.color = UnityEngine.Random.Range(0, 5);
        this.activePieceData.orient = 0;
        this.activePiece.Initialize(this.spawnPosition, data);

        if (IsValidPosition(this.activePiece, this.spawnPosition, 0)){
            Set(this.activePiece);
        } else {
            GameOver();
        }

        Set(this.activePiece);
    }

    
    public void SpawnPieceHold(){
        TetrominoData data = this.tetrominos[activePieceData.type];
        Vector2Int[] holdData = new Vector2Int[4];
        for (int i = 0; i < 4; i ++){
            holdData[i] = Data.originalOrient[holdPieceData.type * 4, i];
        }

        this.activePiece.Initialize(this.spawnPosition, data);
        
        for (int i = 0; i < 4; i ++){
            this.holdPiece[i] = (Vector3Int)holdData[i];
        }


        if (IsValidPosition(this.activePiece, this.spawnPosition, 0)){
            Set(this.activePiece);
        } else {
            GameOver();
        }

        Set(this.activePiece);
    }
    

    
    public void HoldPieceInitialize(){
        Vector2Int[] data = new Vector2Int[4];
        for (int i = 0; i < 4; i ++){
            data[i] = Data.originalOrient[holdPieceData.type * 4, i];
        }
    
        for (int i = 0; i < data.Length; i ++){
            this.holdPiece[i] = (Vector3Int)data[i];
        }
    }
    

    private void GameOver(){
        ResetGame?.Invoke(this, EventArgs.Empty);
        this.tilemap.ClearAllTiles();
        holdStart = true;
    }

    public void Set(Piece piece){
        for (int i = 0; i < pieceSize; i++){
            Vector3Int tilePosition = (Vector3Int)Data.originalOrient[(activePieceData.type * 4) + activePieceData.orient, i] + piece.position;      //piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, this.tileColor[this.activePieceData.color]);
        }
    }

    
    public void SetHold(Vector3Int[] holdPiece){
        for (int i = 0; i < holdPiece.Length; i++){
            Vector3Int holdGrid = holdPiece[i] + holdPosition;
            this.tilemap.SetTile(holdGrid, this.tileColor[this.holdPieceData.color]);
        }
    }
    

    public void Clear(Piece piece){
        for (int i = 0; i < pieceSize; i++){
            Vector3Int tilePosition = (Vector3Int)Data.originalOrient[(activePieceData.type * 4) + activePieceData.orient, i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    
    public void Hold(Piece piece){
        if (holdOnce){
            if (holdStart){
                Clear(this.activePiece);
                holdPieceData.type = activePieceData.type;
                holdPieceData.color = activePieceData.color;
                holdStart = false;
                HoldPieceInitialize();
                SpawnPiece();
            } else {
                Clear(this.activePiece);
                temp = holdPieceData.type;
                holdPieceData.type = activePieceData.type;
                activePieceData.type = temp;
                temp = holdPieceData.color;
                holdPieceData.color = activePieceData.color;
                activePieceData.color = temp;
                SpawnPieceHold();
            }
            ClearHold();
            SetHold(holdPiece);
        }
        
        holdOnce = false;
    }
    

    
    public void ClearHold(){
        for (int i = -2; i < 4; i++){
            for (int j = -2; j < 3; j ++){
                Vector3Int holdGrid = holdPosition + new Vector3Int(i, j, 0);
                this.tilemap.SetTile(holdGrid, null);
            }
        }
    }
    

    public bool IsValidPosition(Piece piece, Vector3Int position, int orient){
        RectInt bounds  = this.Bounds;
        bounds.height = bounds.height + 2;

        for (int i = 0; i < pieceSize; i++){
            Vector3Int tilePosition = (Vector3Int)Data.originalOrient[this.activePieceData.type * 4 + orient, i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition)){
                return false;
            }

            if (this.tilemap.HasTile(tilePosition)){
                return false;
            }
        }

        return true;
    }

    public void ClearLines(){
        if (buffs.totalBuffs.StopClearing){
            return;
        }

        RectInt bounds = this.Bounds;
        int row = bounds.yMin;

        while (row < bounds.yMax){
            if (IsLineFull(row)){
                GetLineColor(row);
                LineClear(row);
                LineCleared?.Invoke(this, new LineClearedEventArgs { colorArray = this.colorArray } );
            } else {
                row++;
            }
        }


    }

    public void ClearColorArray(){
        for (int i = 0; i < 5; i++){
            colorArray[i] = 0;
        }
    }

    public void GetLineColor(int row){
        RectInt bounds = this.Bounds;
        TileBase tileName;
        string name;
        ClearColorArray();

        for (int col = bounds.xMin; col < bounds.xMax; col++){
            Vector3Int position = new Vector3Int(col, row, 0);
            tileName = tilemap.GetTile(position);
            name = tileName.ToString();
            if (ColorIndex(name) != 5){
                colorArray[ColorIndex(name)] += 1;
            }
        }
    }

    public int ColorIndex(string name){
        switch (name)
        {
            case "Red (UnityEngine.Tilemaps.Tile)":
                return 0;
            case "Blue (UnityEngine.Tilemaps.Tile)":
                return 1;
            case "Green (UnityEngine.Tilemaps.Tile)":
                return 2;
            case "Yellow (UnityEngine.Tilemaps.Tile)":
                return 3;
            case "Purple (UnityEngine.Tilemaps.Tile)": 
                return 4;
            default:
                return 5;
        }
    }

    private bool IsLineFull(int row){
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++){
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!this.tilemap.HasTile(position)){
                return false;
            }
        }

        return true; 
    }

    private void LineClear(int row){
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++){
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax){
            for (int col = bounds.xMin; col < bounds.xMax; col++){
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = this.tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                this.tilemap.SetTile(position, above);


            }

            row ++;
        }
    }

    private void When_BuffDisappeared_LineClear(object sender, Buffs.BuffDisappearedEventArgs e){
        if (e.id != CharacterData.BuffName.StopClearing){
            return;
        }

        ClearLines();
    }

}
