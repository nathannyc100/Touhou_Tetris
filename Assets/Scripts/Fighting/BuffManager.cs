using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BuffManager : MonoBehaviour {
    public List<Buff> selfBuffList;
    public List<Buff> enemyBuffList;

    private NetworkTimingManager networkTimingManager;

    private void Awake() {
        networkTimingManager = NetworkTimingManager.Singleton;
    }

    private void OnEnable() {
        networkTimingManager.TimeIncrement += When_TimeIncrement;
    }

    public void AddSelfBuff(Buff buff) {
        selfBuffList.Add(buff);
    }

    public void AddEnemyBuff(Buff buff) {
        enemyBuffList.Add(buff);
    }

    public void ClearSelfBuffs() {
        selfBuffList.Clear();
    }

    public void ClearEnemyBuffs() {
        enemyBuffList.Clear();
    }

    private void When_TimeIncrement(object sender, NetworkTimingManager.TimeIncrementEventArgs e) {
        foreach (Buff buff in selfBuffList) {
            buff.duration --;
        }

        foreach (Buff buff in enemyBuffList) {  
            buff.duration --;
        }
    }
}

public class TotalBuffs {
    public bool[] SpellBind;
    public bool SpellBindAll;
    public float DamageMultiplier;
    public float DefenseMultiplier;
    public float[] BlockColorSpawnPercentage;
    public float[] BlockColorDamageBuff;
    public float HealBuff;
    public float ManaRegen;
    public bool[] KeypressBan;
    public bool ClearBan;
    public bool Invisibility;
    public float SpellEffect;
    public bool StopClearing;
    public bool StopRegularAttack;
    public bool StopTime;
}

public class Buff {
    public BuffType type;
    public float amount;
    public int duration;
    public float[] array;
}

public enum BuffType {
    SpellBind,
    SpellBindAll,
    BuffMultiplier,
    DebuffMultiplier,
    BuffPercentace,
    DebuffPercentage,
    ChangeSelfColorPercentage,
    ChangeAllColorPercentage,
    StopRegularAttack,
    ChangeManaRegen,
    BanKeypress,
    StopClearing,
    BlockColorAttack,
    Invisibility,
    SkillBuff,
    StopTime,
    Weaken,
    StopHeal,
}
