using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Board : MonoBehaviour {
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public Vector3Int[] holdPiece;
    public TileBase[] tileColor;
    public TetrominoData[] tetrominos;
    public Vector3Int spawnPosition;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public int color;
    public int[] colorArray; // red blue green yellow purple
    public int character;
    public int damage { get; private set; }
    public int health;
    public TextMeshProUGUI text;
    public TetrominoData holdTetromino { get; private set; }
    public bool holdStart = true;
    public bool holdOnce = true;
    public int[] activePieceData = new int[2];
    public int[] holdPieceData = new int[2];
    public int skillPoints;
    
    public Vector3Int holdPosition = new Vector3Int(-10, 5);

    public Controls controls;


    public RectInt Bounds {
        get {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake() {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        this.controls = new Controls();

        for (int i = 0; i < this.tetrominos.Length; i ++){
            this.tetrominos[i].Initialize();
        }

    }

    private void OnEnable(){
        controls.Enable();
    }

    private void OnDisable(){
        controls.Disable();
    }

    public void Start(){
        PrintHealth();
        SpawnPiece();

    }

    public void SwapArray(int[] array1, int[] array2, int arrayLen){
        int temp;
        for (int i = 0; i < arrayLen; i ++){
            temp = array1[i];
            array1[i] = array2[i];
            array2[i] = temp;
        }
    }

    public void SpawnPiece(){
        this.activePieceData[0] = Random.Range(0, this.tetrominos.Length);
        TetrominoData data = this.tetrominos[this.activePieceData[0]];
        this.activePieceData[1] = Random.Range(0, 5);

        this.activePiece.Initialize(this, this.spawnPosition, data);

        if (IsValidPosition(this.activePiece, this.spawnPosition)){
            Set(this.activePiece);
        } else {
            GameOver();
        }

        Set(this.activePiece);
    }

    
    public void SpawnPieceHold(){
        TetrominoData data = this.tetrominos[activePieceData[0]];
        Vector2Int[] holdData = Data.Cells[(Tetromino)holdPieceData[0]];

        this.activePiece.Initialize(this, this.spawnPosition, data);
        
        for (int i = 0; i < 4; i ++){
            this.holdPiece[i] = (Vector3Int)holdData[i];
        }


        if (IsValidPosition(this.activePiece, this.spawnPosition)){
            Set(this.activePiece);
        } else {
            GameOver();
        }

        Set(this.activePiece);
    }
    

    
    public void HoldPieceInitialize(){
        Vector2Int[] data = Data.Cells[(Tetromino)holdPieceData[0]];
    
        for (int i = 0; i < data.Length; i ++){
            this.holdPiece[i] = (Vector3Int)data[i];
        }
    }
    

    private void GameOver(){
        this.tilemap.ClearAllTiles();
        holdStart = true;
    }

    public void Set(Piece piece){
        for (int i = 0; i < piece.cells.Length; i++){
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, this.tileColor[this.activePieceData[1]]);
        }
    }

    
    public void SetHold(Vector3Int[] holdPiece){
        for (int i = 0; i < holdPiece.Length; i++){
            Vector3Int holdGrid = holdPiece[i] + holdPosition;
            this.tilemap.SetTile(holdGrid, this.tileColor[this.holdPieceData[1]]);
        }
    }
    

    public void Clear(Piece piece){
        for (int i = 0; i < piece.cells.Length; i++){
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    
    public void Hold(Piece piece){
        if (holdOnce){
            if (holdStart){
                Clear(this.activePiece);
                for (int i = 0; i < 2; i ++){
                    holdPieceData[i] = activePieceData[i];
                }
                holdStart = false;
                HoldPieceInitialize();
                SpawnPiece();
            } else {
                Clear(this.activePiece);
                SwapArray(holdPieceData, activePieceData, 2);
                SpawnPieceHold();
            }
            ClearHold();
            SetHold(holdPiece);
        }
        
        holdOnce = false;
    }
    

    
    public void ClearHold(){
        for (int i = -2; i < 3; i++){
            for (int j = -2; j < 3; j ++){
                Vector3Int holdGrid = holdPosition + new Vector3Int(i, j, 0);
                this.tilemap.SetTile(holdGrid, null);
            }
        }
    }
    

    public bool IsValidPosition(Piece piece, Vector3Int position){
        RectInt bounds  = this.Bounds;

        for (int i = 0; i < piece.cells.Length; i++){
            Vector3Int tilePosition = piece.cells[i] + position;

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
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;

        while (row < bounds.yMax){
            if (IsLineFull(row)){
                GetLineColor(row);
                DamageCalc(character, colorArray);
                PrintHealth();
                LineClear(row);
            } else {
                row++;
            }
        }


    }

    
    public void DamageCalc(int character, int[] color){
        damage = 0;
        float attack = Data.Characters[character].attack;
        float[] multiplier = Data.Characters[character].multiplier;

        for (int i = 0; i < 5; i ++){
            damage += Mathf.CeilToInt(attack * color[i] * multiplier[i]);
        }
        
        health -= damage;
    }
    
    public void PrintHealth(){
        text.text = health.ToString();
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
        for (int col = bounds.xMin; col < bounds.xMax; col++){
            Vector3Int position = new Vector3Int(col, row, 0);
            tileName = tilemap.GetTile(position);
            name = tileName.ToString();
            if (ColorIndex(name) != 5){
                colorArray[ColorIndex(name)] += 1;
            }
        }
        for (int i = 0; i < 5; i++){
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

}
