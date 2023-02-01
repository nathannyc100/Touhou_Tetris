using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterData
{
    public enum ConstructName {
        SpellBind,
        SpellBindAll,
        SelfBuffMultiplier,
        SelfDebuffMultiplier,
        SelfBuffPercentace,
        SelfDebuffPercentage,
        EnemyBuffMultiplier,
        EnemyDebuffMultiplier,
        EnemyBuffPercentage,
        EnemyDebuffPercentage,
        DamageFixed,
        DamageChanging,
        RemoveSelfBlocks,
        RemoveEnemyBlocks,
        ChangeSelfColorPercentage,
        ChangeAllColorPercentage,
        ChangeBlockColor,
        StopRegularAttack,
        HealFixed,
        HealChangeing,
        ChangeManaRegen,
        StealHold,
        BanKeypress,
        StopClearing,
        BlockColorAttackBuff,
        Invisibility,
        SkillBuff,
        StopTime,
        Weaken,
        TrapBlocks,
    }

    public enum BuffName {
        SpellBind,
        SpellBindAll,
        SelfBuffMultiplier,
        SelfDebuffMultiplier,
        SelfBuffPercentace,
        SelfDebuffPercentage,
        EnemyBuffMultiplier,
        EnemyDebuffMultiplier,
        EnemyBuffPercentage,
        EnemyDebuffPercentage,
        ChangeSelfColorPercentage,
        ChangeAllColorPercentage,
        StopRegularAttack,
        ChangeManaRegen,
        BanKeypress,
        StopClearing,
        BlockColorAttackBuff,
        Invisibility,
        SkillBuff,
        StopTime,
        Weaken,
    }

    public enum BlockRemoveOrient {
        Horizontal, 
        Vertical,
        Random,
    }

    public enum DependentValues {
        BlocksRemoved,
    }

    public enum SkillName {
        Skill1,
        Skill2,
        Skill3,
        Skill4,
        Skill5,
        SkillFinal,
    }

    public enum Color {
        Red,
        Blue,
        Green,
        Yellow,
        Purple,
    }

    public enum Keypress {
        Left,
        Right,
        Up,
        Down,
        Space,

    }

    public class SkillData {
        
        public int manaCost;
        public string skillName;
        public int CD;
        public SkillConstruct[] construct;
    }

    public class SkillConstruct {
        public ConstructName id;
        public int duration;
        public float percentage;
        public int amount;
        public bool stacking;
        public BlockRemoveOrient blockOrient;
        public DependentValues dependentValue;
        public Color blockColor;
        public Keypress keypress;
        public string other;
    }

    public class Character
    {
        public string name;
        public int health;
        public float attack;
        public float[] multiplier;
    }
    
    public static readonly Dictionary<int, Character> Characters = new Dictionary<int, Character>(){
        { 0, new Character { name = "Test Character", health = 50000, attack = 1f, multiplier = new float[]{ 1.0f, 1.0f, 1.0f, 1.0f, 1.0f } } },
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

    public static readonly SkillData[,] skillData = new SkillData[,]{
        {
            new SkillData {},
            new SkillData {},
            new SkillData {},
            new SkillData {},
            new SkillData {},
            new SkillData {},
        },
        {   
            new SkillData { manaCost = 15, skillName = "夢符[封魔陣]", CD = 60, construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.SpellBind, amount = 2, duration = 20 } } }, 
            new SkillData { manaCost = 10, skillName = "空中飛翔的不可思議巫女", construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.SelfBuffMultiplier, amount = 2, duration = 20 }, new SkillConstruct { id = ConstructName.SelfDebuffMultiplier, amount = 2, duration = 20 } } },
            new SkillData { manaCost = 25, skillName = "神技[八方龍殺陣]", construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.RemoveSelfBlocks, amount = 4, duration = 0, blockOrient = BlockRemoveOrient.Horizontal }, new SkillConstruct { id = ConstructName.DamageChanging, amount = 50, duration = 0, dependentValue = DependentValues.BlocksRemoved }, new SkillConstruct { id = ConstructName.ChangeAllColorPercentage, duration = 0, percentage = 0.15f, blockColor = Color.Red }, new SkillConstruct { id = ConstructName.ChangeAllColorPercentage, duration = 0, percentage = -0.15f, blockColor = Color.Purple } } },
            new SkillData { manaCost = 15, skillName = "靈符[陰陽玉]", construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.StopRegularAttack, duration = 20 } } },
            new SkillData { manaCost = 15, skillName = "夢想天生", construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.HealFixed, amount = 5000 }, new SkillConstruct { id = ConstructName.ChangeManaRegen, amount = 2, duration = 15 } } },
            new SkillData { manaCost = 0, skillName = "靈符[夢想封印]", construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.SpellBindAll, duration = 40 }, new SkillConstruct { id = ConstructName.DamageFixed, amount = 3000 } } },
        },
        {
            new SkillData { manaCost = 15, skillName = "魔符[星塵幻想]", construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.ChangeAllColorPercentage, duration = 30, percentage = 0f, blockColor = Color.Blue }, new SkillConstruct { id = ConstructName.ChangeAllColorPercentage, duration = 30, percentage = 0f, blockColor = Color.Green }, new SkillConstruct { id = ConstructName.ChangeAllColorPercentage, duration = 30, percentage = 0f, blockColor = Color.Purple }, new SkillConstruct { id = ConstructName.ChangeAllColorPercentage, duration = 30, percentage = 0f, blockColor = Color.Red } } },
            new SkillData { manaCost = 10, skillName = "魔理沙偷走了重要的東西", construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.StealHold } } },
            new SkillData { manaCost = 20, skillName = "黑魔[黑洞邊緣]", construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.BanKeypress, duration = 20, keypress = Keypress.Left }, new SkillConstruct { id = ConstructName.BanKeypress, duration = 20, keypress = Keypress.Right }, new SkillConstruct { id = ConstructName.SelfDebuffPercentage, duration = 20, percentage = 0.2f }, new SkillConstruct { id = ConstructName.ChangeSelfColorPercentage, duration = 20, percentage = 2.0f, blockColor = Color.Yellow } } },
            new SkillData { manaCost = 15, skillName = "戀風[星光颱風]", construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.BlockColorAttackBuff, duration = 30, percentage = 1.5f } } },
            new SkillData { manaCost = 15, skillName = "戀符[Master Spark]", construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.RemoveEnemyBlocks, amount = 3, blockOrient = BlockRemoveOrient.Horizontal }, new SkillConstruct { id = ConstructName.DamageChanging, amount = 100, dependentValue = DependentValues.BlocksRemoved } } },
            new SkillData { manaCost = 0, skillName = "魔砲[Final Master Spark]", construct = new SkillConstruct[] { new SkillConstruct { id = ConstructName.RemoveEnemyBlocks, amount = 20, blockOrient = BlockRemoveOrient.Horizontal }, new SkillConstruct { id = ConstructName.DamageChanging, amount = 200, dependentValue = DependentValues.BlocksRemoved } } },
        },
    };




}
