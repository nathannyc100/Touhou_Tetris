using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System;

public class Piece : MonoBehaviour {

    private Board board;
    private ControlsManager controlsManager;
    private Ghost ghost;

    public TetrominoData data;
    public Vector3Int[] cells;
    public Vector3Int position;
    public int rotationIndex;
    public float inputTimer;
    public float inputInitialTimer;

    public float stepDelay = 1f;
    public float lockDelay = 0.5f;

    public float moveDelay = 0.3f;
    public float moveSpeed = 0.1f;

    private float previousTime;
    private float lockTime;
    private bool softToggle;
    private bool continuousLeft;
    private bool continuousRight;

    public void Initialize(Vector3Int position, TetrominoData data){
        this.position = position;
        this.data = data;
        this.rotationIndex = 0;
        this.previousTime = Time.time;
        this.lockTime= 0f;

        if (this.cells.Length != data.cells.Length){
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i ++){
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    private void OnEnable(){
        this.board = DependencyManager.instance.board;
        this.controlsManager = DependencyManager.instance.controlsManager;
        this.ghost = DependencyManager.instance.ghost;

        controlsManager.OnKeyPressed += When_OnKeyPressed;

    }

    private void Update(){
        this.board.Clear(this);

        this.lockTime += Time.deltaTime;

        if (continuousLeft){
            if (Time.time - inputTimer > moveSpeed){
                inputTimer = Time.time;
                Move(Vector2Int.left);
            }
        }

        if (continuousRight){
            if (Time.time - inputTimer > moveSpeed){
                inputTimer = Time.time;
                Move(Vector2Int.right);
            }
        }

        Fall();

        this.board.Set(this);
    }


    // Piece falling mechanism 
    private void Fall(){
        if (GameManager.GameCurrentState == GameManager.GameState.CountdownScreen){
            return;
        }
        
        if (softToggle == false){
            if (Time.time - this.previousTime > stepDelay){
                Step();
            }
        } else if (softToggle == true){
            if (Time.time - this.previousTime > stepDelay / 10){
                Step();
            }
        }

    }

    
    private void OnHold(){
        this.ghost.Clear();
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

    private bool Move(Vector2Int translation){
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.IsValidPosition(this, newPosition, board.activePieceData.orient);

        if (valid) {
            this.position = newPosition;
            this.lockTime = 0f;
        }

        return valid;
    }

    private void Rotate(int direction){
        int finalOrient = board.activePieceData.orient + direction;

        if (finalOrient == -1){
            finalOrient = 3;
        } else if (finalOrient == 4){
            finalOrient = 0;
        }

        if (TestSRS(finalOrient)){
            ghost.Clear();
            board.activePieceData.orient = finalOrient;

        }
    }

    private bool TestSRS(int finalOrient){
        if (board.IsValidPosition(this, this.position, finalOrient)){
            return true;
        }
        for (int i = 0; i < 4; i ++){
            if (board.IsValidPosition(this, (Vector3Int)Data.SRS[(board.activePieceData.type - 1) * 32 + finalOrient * 4 + i] + this.position, finalOrient)){
                Vector3Int temp = this.position;
                temp.x += Data.SRS[(board.activePieceData.type - 1) * 32 + finalOrient * 4 + i].x;
                temp.y += Data.SRS[(board.activePieceData.type - 1) * 32 + finalOrient * 4 + i].y;
                this.position = temp;
                return true;
            }
        }

        return false;
    }

    private void When_OnKeyPressed(object sender, ControlsManager.OnKeyPressedEventArgs e){
        
        this.board.Clear(this);
        

        switch (e.action) {
            case ControlsManager.ActionName.LeftPressed :
                Move(Vector2Int.left);
                break;
            case ControlsManager.ActionName.LeftHeld :
                continuousLeft = true;
                break;
            case ControlsManager.ActionName.RightPressed :
            Move(Vector2Int.right);
                break;
            case ControlsManager.ActionName.RightHeld :
                continuousRight = true;
                break;
            case ControlsManager.ActionName.HorizontalCancelled :
                continuousLeft = false;
                continuousRight = false;
                break;
            case ControlsManager.ActionName.SoftDropPressed :
                softToggle = true;
                break;
            case ControlsManager.ActionName.SoftDropCancelled :
                softToggle = false;
                break;
            case ControlsManager.ActionName.HardDrop :
                HardDrop();
                break;
            case ControlsManager.ActionName.RotateLeft :
                Rotate(-1);
                break;
            case ControlsManager.ActionName.RotateRight :
                Rotate(1);
                break;
            case ControlsManager.ActionName.Hold :
                OnHold();
                break;
            default :
                break;

        }

        this.board.Set(this);
    }
     
}
