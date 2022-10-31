using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static readonly float cos = Mathf.Cos(Mathf.PI / 2f);
    public static readonly float sin = Mathf.Sin(Mathf.PI / 2f);
    public static readonly float[] RotationMatrix = new float[] { cos, sin, -sin, cos };
    
    public class Character
    {
        public string name; 
        public int health;
        public float attack;
        public float[] multiplier;
    };

    
    public static readonly Dictionary<int, Character> Characters = new Dictionary<int, Character>(){
        { 1, new Character { name = "博麗靈夢", health = 50000, attack = 5.0f, multiplier = new float[]{ 2.0f, 1.0f, 1.0f, 1.0f, 0.75f } } },
    };

    public static readonly Dictionary<Tetromino, Vector2Int[]> Cells = new Dictionary<Tetromino, Vector2Int[]>()
    {
        { Tetromino.I, new Vector2Int[] { new Vector2Int( 0, 2), new Vector2Int( 1, 2), new Vector2Int( 2, 2), new Vector2Int( 3, 2) } },
        { Tetromino.J, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 0, 2) } },
        { Tetromino.L, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 2, 2) } },
        { Tetromino.O, new Vector2Int[] { new Vector2Int( 1, 1), new Vector2Int( 1, 2), new Vector2Int( 2, 1), new Vector2Int( 2, 2) } },
        { Tetromino.S, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 1, 2), new Vector2Int( 2, 2) } },
        { Tetromino.T, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 1, 2) } },
        { Tetromino.Z, new Vector2Int[] { new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 0, 2), new Vector2Int( 1, 2) } },
    };

    private static readonly Vector2Int[,] WallKicksI = new Vector2Int[,] {
        { new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2,-1), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 1), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 2), new Vector2Int( 2,-1) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1,-2), new Vector2Int(-2, 1) },
        { new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 1), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2,-1), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1,-2), new Vector2Int(-2, 1) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 2), new Vector2Int( 2,-1) },
    };

    private static readonly Vector2Int[,] WallKicksJLOSTZ = new Vector2Int[,] {
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0,-2), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,-1), new Vector2Int(0, 2), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,-1), new Vector2Int(0, 2), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0,-2), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int(0,-2), new Vector2Int( 1,-2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,-1), new Vector2Int(0, 2), new Vector2Int(-1, 2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,-1), new Vector2Int(0, 2), new Vector2Int(-1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int(0,-2), new Vector2Int( 1,-2) },
    };

    /** 

    north
        rotate left 
            5
        rotate right 
            5
    east 
        roatate left 
            5
        rotate right 
            5
    south
        rotate left 
            5
        rotate right 
            5
    west 
        rotate left 
            5
        rotate right 
            5
    **/

    private static readonly Vector2Int[,] SRSI = new Vector2Int[,] {
        //nl
        { new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int( 1, 2), new Vector2Int( 1, 3), new Vector2Int( 0, 0) },
        { new Vector2Int( 0, 0), new Vector2Int( 0, 1), new Vector2Int( 0, 2), new Vector2Int( 0, 3), new Vector2Int( 1, 0) },
        { new Vector2Int( 3, 0), new Vector2Int( 3, 1), new Vector2Int( 3, 2), new Vector2Int( 3, 3), new Vector2Int(-2, 0) },
        { new Vector2Int( 0, 2), new Vector2Int( 0, 3), new Vector2Int( 0, 4), new Vector2Int( 0, 5), new Vector2Int( 1,-2) },
        { new Vector2Int( 3, 2), new Vector2Int( 3, 1), new Vector2Int( 3, 0), new Vector2Int( 3,-1), new Vector2Int(-2, 1) },
        //nr
        { new Vector2Int( 2, 0), new Vector2Int( 2, 1), new Vector2Int( 2, 2), new Vector2Int( 2, 3), new Vector2Int( 0, 0) },
        { new Vector2Int( 0, 0), new Vector2Int( 0, 1), new Vector2Int( 0, 2), new Vector2Int( 0, 3), new Vector2Int( 2, 0) },
        { new Vector2Int( 3, 0), new Vector2Int( 3, 1), new Vector2Int( 3, 2), new Vector2Int( 3, 3), new Vector2Int(-1, 0) },
        { new Vector2Int( 0,-1), new Vector2Int( 0, 0), new Vector2Int( 0, 1), new Vector2Int( 0, 2), new Vector2Int( 2, 1) },
        { new Vector2Int( 3, 2), new Vector2Int( 3, 3), new Vector2Int( 3, 4), new Vector2Int( 3, 5), new Vector2Int(-1,-2) },
        //el
        { new Vector2Int( 0, 2), new Vector2Int( 1, 2), new Vector2Int( 2, 2), new Vector2Int( 3, 2), new Vector2Int( 0, 0) },
        { new Vector2Int( 2, 2), new Vector2Int( 3, 2), new Vector2Int( 4, 2), new Vector2Int( 5, 2), new Vector2Int(-2, 0) },
        { new Vector2Int(-1, 2), new Vector2Int( 0, 2), new Vector2Int( 1, 2), new Vector2Int( 2, 2), new Vector2Int( 1, 0) },
        { new Vector2Int( 2, 3), new Vector2Int( 3, 3), new Vector2Int( 4, 3), new Vector2Int( 5, 3), new Vector2Int(-2,-1) },
        { new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0), new Vector2Int( 2, 0), new Vector2Int( 1, 2) },
        //er
        { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 3, 1), new Vector2Int( 0, 0) },
        { new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 1, 0) },
        { new Vector2Int( 2, 1), new Vector2Int( 3, 1), new Vector2Int( 4, 1), new Vector2Int( 5, 1), new Vector2Int(-2, 0) },
        { new Vector2Int(-1, 3), new Vector2Int( 0, 3), new Vector2Int( 1, 3), new Vector2Int( 2, 3), new Vector2Int( 1,-2) },
        { new Vector2Int( 2, 0), new Vector2Int( 3, 0), new Vector2Int( 4, 0), new Vector2Int( 5, 0), new Vector2Int(-2, 1) },
        //sl
        { new Vector2Int( 2, 0), new Vector2Int( 2, 1), new Vector2Int( 2, 2), new Vector2Int( 2, 3), new Vector2Int( 0, 0) },
        { new Vector2Int( 3, 0), new Vector2Int( 3, 1), new Vector2Int( 3, 2), new Vector2Int( 3, 3), new Vector2Int(-1, 0) },
        { new Vector2Int( 0, 0), new Vector2Int( 0, 1), new Vector2Int( 0, 2), new Vector2Int( 0, 3), new Vector2Int( 2, 0) },
        { new Vector2Int( 3,-2), new Vector2Int( 3,-1), new Vector2Int( 3, 0), new Vector2Int( 3, 1), new Vector2Int(-1,-2) },
        { new Vector2Int( 0, 1), new Vector2Int( 0, 2), new Vector2Int( 0, 3), new Vector2Int( 0, 4), new Vector2Int( 2,-1) },
        //sr
        { new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int( 1, 2), new Vector2Int( 1, 3), new Vector2Int( 0, 0) },
        { new Vector2Int( 3, 0), new Vector2Int( 3, 1), new Vector2Int( 3, 2), new Vector2Int( 3, 3), new Vector2Int(-2, 0) },
        { new Vector2Int( 0, 0), new Vector2Int( 0, 1), new Vector2Int( 0, 2), new Vector2Int( 0, 3), new Vector2Int( 1, 0) },
        { new Vector2Int( 3, 1), new Vector2Int( 3, 2), new Vector2Int( 3, 3), new Vector2Int( 3, 4), new Vector2Int(-2,-1) },
        { new Vector2Int( 0,-2), new Vector2Int( 0,-1), new Vector2Int( 0, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 2) },
        //wl
        { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 3, 1), new Vector2Int( 0, 0) },
        { new Vector2Int(-2, 1), new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 0) },
        { new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 3, 1), new Vector2Int( 4, 1), new Vector2Int(-1, 0) },
        { new Vector2Int(-2, 0), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0), new Vector2Int( 2, 1) },
        { new Vector2Int( 1, 3), new Vector2Int( 2, 3), new Vector2Int( 3, 3), new Vector2Int( 4, 3), new Vector2Int(-1,-2) },
        //wr
        { new Vector2Int( 0, 2), new Vector2Int( 1, 2), new Vector2Int( 2, 2), new Vector2Int( 3, 2), new Vector2Int( 0, 0) },
        { new Vector2Int( 1, 2), new Vector2Int( 2, 2), new Vector2Int( 3, 2), new Vector2Int( 4, 2), new Vector2Int(-1, 0) },
        { new Vector2Int(-2, 2), new Vector2Int(-1, 2), new Vector2Int( 0, 2), new Vector2Int( 1, 2), new Vector2Int( 2, 0) },
        { new Vector2Int( 1, 0), new Vector2Int( 2, 0), new Vector2Int( 3, 0), new Vector2Int( 4, 0), new Vector2Int(-1, 2) },
        { new Vector2Int(-2, 3), new Vector2Int(-1, 3), new Vector2Int( 0, 3), new Vector2Int( 1, 3), new Vector2Int( 0, 0) },
    };
    

    public static readonly Dictionary<Tetromino, Vector2Int[,]> WallKicks = new Dictionary<Tetromino, Vector2Int[,]>()
    {
        { Tetromino.I, WallKicksI },
        { Tetromino.J, WallKicksJLOSTZ },
        { Tetromino.L, WallKicksJLOSTZ },
        { Tetromino.O, WallKicksJLOSTZ },
        { Tetromino.S, WallKicksJLOSTZ },
        { Tetromino.T, WallKicksJLOSTZ },
        { Tetromino.Z, WallKicksJLOSTZ },
    };



}