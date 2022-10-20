using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Piece : MonoBehaviour {

    public Board board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }
    public int rotationIndex { get; private set; }
    public float[] inputManage = new float[2];
    public bool[] inputOnce = new bool[2] {true, true};
    public float[] inputTimer = new float[2];
    public float[] inputInitialTimer = new float[2];

    public float stepDelay = 1f;
    public float lockDelay = 0.5f;
    public float moveDelay = 0.3f;
    public float moveSpeed = 0.1f;

    private float previousTime;
    private float lockTime;
    private float softToggle;

    public void Initialize(Board board, Vector3Int position, TetrominoData data){
        this.board = board;
        this.position = position;
        this.data = data;
        this.rotationIndex = 0;
        this.previousTime = Time.time;
        this.lockTime= 0f;

        if (this.cells == null){
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i ++){
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    private void Update(){
        this.board.Clear(this);

        this.lockTime += Time.deltaTime;

        
        if (this.board.controls.Keyboard.RotateLeft.triggered){
            Rotate(-1);
        }   
        if (this.board.controls.Keyboard.RotateRight.triggered){
            Rotate(1);
        }

        this.inputManage[0] = this.board.controls.Keyboard.Left.ReadValue<float>();
        this.inputManage[1] = this.board.controls.Keyboard.Right.ReadValue<float>();

        if (this.inputManage[0] == 1){
            if (inputOnce[0]){
                Move(Vector2Int.left);
                inputOnce[0] = false;
                inputInitialTimer[0] = Time.time;
                inputTimer[0] = Time.time + moveDelay - moveSpeed;
            } else {
                if (Time.time - inputInitialTimer[0] > moveDelay && Time.time - inputTimer[0] > moveSpeed){
                    inputTimer[0] = Time.time;
                    Move(Vector2Int.left);
                }
            }
        } else if (inputManage[0] == 0){
            inputOnce[0] = true;
        }

        if (this.inputManage[1] == 1){
            if (inputOnce[1]){
                Move(Vector2Int.right);
                inputOnce[1] = false;
                inputInitialTimer[1] = Time.time;
                inputTimer[1] = Time.time + moveDelay - moveSpeed;
            } else {
                if (Time.time - inputInitialTimer[1] > moveDelay && Time.time - inputTimer[1] > moveSpeed){
                    inputTimer[1] = Time.time;
                    Move(Vector2Int.right);
                }
            }
        } else if (inputManage[1] == 0){
            inputOnce[1] = true;
        }

        if (this.board.controls.Keyboard.HardDrop.triggered){
            HardDrop();
        }
            
        softToggle = this.board.controls.Keyboard.SoftDrop.ReadValue<float>();
        
        if (softToggle == 0){
            if (Time.time - this.previousTime > stepDelay){
                Step();
            }
        } else if (softToggle == 1){
            if (Time.time - this.previousTime > stepDelay / 10){
                Step();
            }
        }
        

        this.board.Set(this);
    }

    
    private void OnHold(){
        this.board.Hold(this);
    }

    private void Step(){
        this.previousTime = Time.time;

        Move(Vector2Int.down);

        if (this.lockTime >= this.lockDelay){
            Lock();
        }
    }

    private void HardDrop(){
        while (Move(Vector2Int.down)){
            continue;
        }

        Lock();
    }

    private void Lock(){
        this.board.Set(this);
        this.board.ClearLines();
        this.board.SpawnPiece();
        this.board.holdOnce = true;
    }

    private bool  Move(Vector2Int translation){
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.IsValidPosition(this, newPosition);

        if (valid) {
            this.position = newPosition;
            this.lockTime = 0f;
        }

        return valid;
    }

    private void Rotate(int direction){
        int orignalRotation = this.rotationIndex;
        this.rotationIndex = Wrap(this.rotationIndex + direction, 0 ,4);

        ApplyRotationMatrix(direction);

        if (!TestWallKicks(this.rotationIndex, direction)){
            this.rotationIndex = orignalRotation;
            ApplyRotationMatrix(-direction);
        }
        
    }

    private void ApplyRotationMatrix(int direction){
        for (int i = 0; i < this.cells.Length; i++){
            Vector3 cell = this.cells[i];

            int x, y;

            switch (this.data.tetromino) {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    break;
            }

            this.cells[i] = new Vector3Int(x, y, 0);
        }

    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection){
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

        for (int i = 0; i < this.data.wallKicks.GetLength(1); i++){
            Vector2Int translation = this.data.wallKicks[wallKickIndex, i];

            if (Move(translation)){
                return true;
            }
        }

        return false; 
    }

    private int GetWallKickIndex(int rotationIndex, int rotationDirection){
        int wallKickIndex = rotationIndex * 2;
        if (rotationDirection < 0) {
            wallKickIndex --;
        }

        return Wrap(wallKickIndex, 0, this.data.wallKicks.GetLength(0));
    }

    private int Wrap(int input, int min, int max){
        if (input < min){
            return max - (min - input) % (max - min);
        } else {
            return min + (input - min) % (max - min);
        }
    }
     
}
