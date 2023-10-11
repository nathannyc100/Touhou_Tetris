using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : MonoBehaviour
{
    public int[] skillMana;
    public int[] skillDuration;
    public int[] skillCD;
    public bool[] isPassive;

    public virtual void Skill1() { }
    public virtual void Skill2() { }
    public virtual void Skill3() { }
    public virtual void Skill4() { }
    public virtual void Skill5() { }
    public virtual void SkillFinal() { }
    
}

public class ReimuHakureSkillData : SkillData {

    private void Awake() {
        skillMana = new int[5] { 0, 15, 25, 15, 30 };
        skillDuration = new int[5] { 0, 0, 10, 10, 10 };
        skillCD = new int[5] { 0, 20, 0, 0, 0 };
        isPassive = new bool[5] { true, false, false, false, false };
    }

    public override void Skill1() { 
    
    }

    public override void Skill2() {

    }

}
