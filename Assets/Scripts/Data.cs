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
    }

    public class PieceData {
        public int type;
        public int color;
        public int orient;
    }

    public class Skills {
        public int type;
        public string name;
        public int stacks;
        public int duration;
        public int intervals;
        public int[] startTime;

    }

    public class Buffs {

    }

    

    
    public static readonly Dictionary<int, Character> Characters = new Dictionary<int, Character>(){
        { 1, new Character { name = "博麗靈夢", health = 50000, attack = 5.0f, multiplier = new float[]{ 2.0f, 1.0f, 1.0f, 1.0f, 0.75f } } },
        { 2, new Character { name = "霧雨魔理沙", health = 46000, attack = 5.4f, multiplier = new float[]{ 1.0f, 0.5f, 0.5f, 3.5f, 0.5f } } },
        { 3, new Character { name = "帕秋莉  諾蕾姬", health = 35000, attack = 5.8f, multiplier = new float[]{ 2.0f, 2.0f, 2.0f, 2.0f, 2.0f } } },
        { 4, new Character { name = "十六夜 咲夜", health = 47000, attack = 4.7f, multiplier = new float[]{ 1.5f, 2.0f, 0.5f, 0.75f, 0.5f } } },
        { 5, new Character { name = "蕾米莉亞 斯卡蕾特", health = 42000, attack = 5.7f, multiplier = new float[]{ 2.5f, 0.5f, 0.75f, 1.0f, 2.0f } } },
        { 6, new Character { name = "芙蘭朵露 斯卡蕾特", health = 39000, attack = 6.0f, multiplier = new float[]{ 3.0f, 0.5f, 0.5f, 1.0f, 0.5f } } },
        { 7, new Character { name = "伊吹翠香", health = 45000, attack = 5.7f, multiplier = new float[]{ 1.5f, 1.5f, 1.5f, 1.5f, 1.5f } } },
        { 8, new Character { name = "古明地覺", health = 50000, attack = 4.5f, multiplier = new float[]{ 2.0f, 0.5f, 1.5f, 1.0f, 1.0f } } },
        { 9, new Character { name = "古明地戀", health = 46000, attack = 5.5f, multiplier = new float[]{ 1.0f, 0.5f, 2.5f, 1.0f, 1.0f } } },
        { 10, new Character { name = "洩矢諏訪子", health = 48000, attack = 5.0f, multiplier = new float[]{ 1.0f, 2.0f, 1.0f, 2.0f, 0.5f } } },
        { 11, new Character { name = "河城荷取", health = 49000, attack = 4.3f, multiplier = new float[]{ 0.5f, 3.0f, 0.5f, 0.5f, 1.0f } } },
        { 12, new Character { name = "藤原妹紅", health = 55000, attack = 5.5f, multiplier = new float[]{ 3.0f, 0.25f, 0.5f, 1.0f, 1.0f } } },
        { 13, new Character { name = "四季映姬 亞瑪薩那度", health = 40000, attack = 5.0f, multiplier = new float[]{ 1.0f, 1.0f, 1.0f, 1.0f, 1.0f } } },
        { 14, new Character { name = "射命丸文", health = 40000, attack = 4.1f, multiplier = new float[]{ 1.5f, 0.5f, 2.0f, 1.0f, 1.5f } } },
        { 15, new Character { name = "比那名居 天子", health = 50000, attack = 5.0f, multiplier = new float[]{ 1.0f, 2.0f, 2.0f, 1.0f, 1.0f } } },
        { 16, new Character { name = "克勞恩皮絲", health = 38000, attack = 5.3f, multiplier = new float[]{ 2.5f, 0.5f, 0.5f, 1.0f, 1.5f } } },
        { 17, new Character { name = "純狐", health = 35000, attack = 6.0f, multiplier = new float[]{ 3.0f, 0.5f, 0.5f, 0.5f, 0.5f } } },
        { 18, new Character { name = "赫卡提亞 拉碧斯拉祖利", health = 54000, attack = 5.6f, multiplier = new float[]{ 1.5f, 1.5f, 1.5f, 1.5f, 1.5f } } },
        { 19, new Character { name = "埴安神袿姬", health = 50000, attack = 4.0f, multiplier = new float[]{ 0.5f, 1.5f, 2.0f, 2.0f, 1.5f } } },
        { 20, new Character { name = "饕餮尤魔", health = 40000, attack = 6.0f, multiplier = new float[]{ 0.5f, 2.5f, 0.5f, 0.5f, 2.5f } } },
        { 21, new Character { name = "魂魄妖夢", health = 40000, attack = 5.0f, multiplier = new float[]{ 1.0f, 1.5f, 2.0f, 1.0f, 1.5f } } },
        { 22, new Character { name = "依神紫苑, 依神女苑", health = 70000, attack = 7.0f, multiplier = new float[]{ 0.5f, 2.0f, 0.25f, 2.5f, 2.5f } } },
        
        
    };

    public static readonly Dictionary<Tetromino, Vector2Int[]> Cells = new Dictionary<Tetromino, Vector2Int[]>()
    {
        { Tetromino.O, new Vector2Int[] { new Vector2Int( 1, 1), new Vector2Int( 1, 2), new Vector2Int( 2, 1), new Vector2Int( 2, 2) } },
        { Tetromino.I, new Vector2Int[] { new Vector2Int( 0, 2), new Vector2Int( 1, 2), new Vector2Int( 2, 2), new Vector2Int( 3, 2) } },
        { Tetromino.T, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 1, 2) } },
        { Tetromino.L, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 2, 2) } },
        { Tetromino.J, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 0, 2) } },
        { Tetromino.S, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 1, 2), new Vector2Int( 2, 2) } },
        { Tetromino.Z, new Vector2Int[] { new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 0, 2), new Vector2Int( 1, 2) } },
    };

    public static readonly Vector2Int[,] originalOrient = new Vector2Int[,] {
        //O
        { new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 1, 2), new Vector2Int( 2, 2) },
        { new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 1, 2), new Vector2Int( 2, 2) },
        { new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 1, 2), new Vector2Int( 2, 2) },
        { new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 1, 2), new Vector2Int( 2, 2) },
        //I
        { new Vector2Int( 0, 2), new Vector2Int( 1, 2), new Vector2Int( 2, 2), new Vector2Int( 3, 2) },
        { new Vector2Int( 2, 0), new Vector2Int( 2, 1), new Vector2Int( 2, 2), new Vector2Int( 2, 3) },
        { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 3, 1) },
        { new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int( 1, 2), new Vector2Int( 1, 3) },
        //T
        { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 1, 2) },
        { new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 1, 2) },
        { new Vector2Int( 1, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1) },
        { new Vector2Int( 1, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 1, 2) },
        //L
        { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 2, 2) },
        { new Vector2Int( 1, 0), new Vector2Int( 2, 0), new Vector2Int( 1, 1), new Vector2Int( 1, 2) },
        { new Vector2Int( 0, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1) },
        { new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int( 0, 2), new Vector2Int( 1, 2) },
        //J
        { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 0, 2) },
        { new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int( 1, 2), new Vector2Int( 2, 2) },
        { new Vector2Int( 2, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1) },
        { new Vector2Int( 0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int( 1, 2) },
        //S
        { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 1, 2), new Vector2Int( 2, 2) },
        { new Vector2Int( 2, 0), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 1, 2) },
        { new Vector2Int( 0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int( 2, 1) },
        { new Vector2Int( 1, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 0, 2) },
        //Z
        { new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 0, 2), new Vector2Int( 1, 2) },
        { new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 2, 2) },
        { new Vector2Int( 1, 0), new Vector2Int( 2, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 1) },
        { new Vector2Int( 0, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 1, 2) },
        
    };

    public static readonly Vector2Int[] SRS = new Vector2Int[] {
        //I north
        new Vector2Int(-2, 0),
        new Vector2Int( 1, 0),
        new Vector2Int(-2,-1),
        new Vector2Int( 1, 2),
        //left
        new Vector2Int(-1, 0),
        new Vector2Int( 2, 0),
        new Vector2Int(-1, 2),
        new Vector2Int( 1,-1),
        //I east
        new Vector2Int(-1, 0),
        new Vector2Int( 2, 0),
        new Vector2Int(-1, 2),
        new Vector2Int( 2,-1),
        //left 
        new Vector2Int( 2, 0),
        new Vector2Int(-1, 0),
        new Vector2Int( 2, 1),
        new Vector2Int(-1,-2),
        //I south
        new Vector2Int( 2, 0),
        new Vector2Int(-1, 0),
        new Vector2Int( 2, 1),
        new Vector2Int(-1,-2),
        //left
        new Vector2Int( 1, 0),
        new Vector2Int(-2, 0),
        new Vector2Int( 1,-2),
        new Vector2Int(-2, 1),
        //I west
        new Vector2Int( 1, 0),
        new Vector2Int(-2, 0),
        new Vector2Int( 1,-2),
        new Vector2Int(-2, 1),
        //left
        new Vector2Int(-2, 0),
        new Vector2Int( 1, 0),
        new Vector2Int(-2,-1),
        new Vector2Int( 1, 2),

        //T north
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int(-1, 1),
        new Vector2Int(-1,-2),
        //left
        new Vector2Int( 1, 0),
        new Vector2Int( 1, 1),
        new Vector2Int( 1, 1),
        new Vector2Int( 1,-2),
        //T east
        new Vector2Int( 1, 0),
        new Vector2Int( 1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int( 1,-2),
        //left
        new Vector2Int( 1, 0),
        new Vector2Int( 1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int( 1, 2),
        //T south
        new Vector2Int( 1, 0),
        new Vector2Int( 0,-2),
        new Vector2Int( 0,-2),
        new Vector2Int( 1,-2),
        //left
        new Vector2Int(-1, 0),
        new Vector2Int( 0,-2),
        new Vector2Int( 0,-2),
        new Vector2Int(-1,-2),
        //T west
        new Vector2Int(-1, 0),
        new Vector2Int(-1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int(-1, 2),
        //left
        new Vector2Int(-1, 0),
        new Vector2Int(-1,-1),
        new Vector2Int( 0,-1),
        new Vector2Int(-1,-1),

        //L north
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int(-1,-2),
        //left
        new Vector2Int( 1, 0),
        new Vector2Int( 1, 1),
        new Vector2Int(-1,-2),
        new Vector2Int( 1,-2),
        //L east
        new Vector2Int( 1, 0),
        new Vector2Int( 1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int( 1, 2),
        //left
        new Vector2Int( 1, 0),
        new Vector2Int( 1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int( 1, 2),
        //L south
        new Vector2Int( 1, 0),
        new Vector2Int( 1, 1),
        new Vector2Int(-1,-2),
        new Vector2Int( 1,-2),
        //left
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int(-1,-2),
        //L west
        new Vector2Int(-1, 0),
        new Vector2Int(-1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int(-1, 2),
        //left
        new Vector2Int(-1, 0),
        new Vector2Int(-1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int(-1, 2),

        //J north
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int(-1,-2),
        //left
        new Vector2Int( 1, 0),
        new Vector2Int( 1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int( 1,-2),
        //J east 
        new Vector2Int( 1, 0),
        new Vector2Int( 1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int( 1, 2),
        //left
        new Vector2Int( 1, 0),
        new Vector2Int( 1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int( 1, 2),
        //J south
        new Vector2Int( 1, 0),
        new Vector2Int( 1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int( 1,-2),
        //left
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int(-1, 2),
        //J west
        new Vector2Int(-1, 0),
        new Vector2Int(-1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int(-1, 2),
        //left
        new Vector2Int(-1, 0),
        new Vector2Int(-1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int(-1, 2),

        //S north
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int(-1, 2),
        //left
        new Vector2Int( 1, 0),
        new Vector2Int( 1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int( 1,-2),
        //S east
        new Vector2Int( 1, 0),
        new Vector2Int( 1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int( 1, 2),
        //left
        new Vector2Int( 1, 0),
        new Vector2Int( 1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int( 1, 2),
        //S south
        new Vector2Int( 1, 0),
        new Vector2Int( 1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int( 1,-2),
        //left
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int(-1,-2),
        //J west
        new Vector2Int(-1, 0),
        new Vector2Int(-1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int(-1, 2),
        //left
        new Vector2Int(-1, 0),
        new Vector2Int(-1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int(-1, 2),

        //Z north
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int(-1,-2),
        //left
        new Vector2Int( 1, 0),
        new Vector2Int( 1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int( 1,-2),
        //Z east
        new Vector2Int( 1, 0),
        new Vector2Int( 1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int( 1, 2),
        //left
        new Vector2Int( 1, 0),
        new Vector2Int( 1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int( 1, 2),
        //Z south
        new Vector2Int( 1, 0),
        new Vector2Int( 1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int( 1,-2),
        //left
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int( 0,-2),
        new Vector2Int(-1,-2),
        //Z west
        new Vector2Int(-1, 0),
        new Vector2Int(-1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int(-1, 2),
        //left
        new Vector2Int(-1, 0),
        new Vector2Int(-1,-1),
        new Vector2Int( 0, 2),
        new Vector2Int(-1, 2),









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