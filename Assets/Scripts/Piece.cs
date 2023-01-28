using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System;

public class Piece : MonoBehaviour {

    public Board board;
    public Ghost ghost;
    public TetrominoData data;
    public Vector3Int[] cells;
    public Vector3Int position;
    public int rotationIndex;
    public float[] inputManage = new float[2];
    public bool[] inputOnce = new bool[2] {true, true};
    public float[] inputTimer = new float[2];
    public float[] inputInitialTimer = new float[2];

    public event EventHandler<OnSkillPressedEventArgs> OnSkillPressed;
    public class OnSkillPressedEventArgs : EventArgs {
        public CharacterData.SkillName id;
    }

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

        if (this.cells.Length != data.cells.Length){
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

        if (this.inputManage[0] == 1 && this.inputManage[1] == 0){
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

        if (this.inputManage[1] == 1 && this.inputManage[0] == 0){
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

        if (this.board.controls.Keyboard.Skill1.triggered){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.Skill1 } );
        }

        if (this.board.controls.Keyboard.Skill2.triggered){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.Skill2 } );
        }

        if (this.board.controls.Keyboard.Skill3.triggered){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.Skill3 } );
        }

        if (this.board.controls.Keyboard.Skill4.triggered){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.Skill4 } );
        }

        if (this.board.controls.Keyboard.Skill5.triggered){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.Skill5 } );
        }

        if (this.board.controls.Keyboard.SkillFinal.triggered){
            OnSkillPressed?.Invoke(this, new OnSkillPressedEventArgs { id = CharacterData.SkillName.SkillFinal } );
        }

        

        this.board.Set(this);
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
     
}
