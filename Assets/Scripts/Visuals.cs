using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Visuals : MonoBehaviour
{
    [SerializeField]
    private Skills skills;
    [SerializeField]
    private Health health;
    [SerializeField]
    private Mana mana;
    [SerializeField]
    private Buffs buffs;

    private void OnEnable(){
        skills.SkillTriggered += When_SkillTriggered;
        skills.NotEnoughMana += When_NotEnoughMana;
        // skills.SkillIsAlreadyOn += When_SkillIsAlreadyOn;
        buffs.BuffDisappeared += When_BuffDisappeared;
    }
    
    private void When_SkillTriggered(object sender, Skills.SkillTriggeredEventArgs e){
        Debug.Log(e.id);
    }

    private void When_NotEnoughMana(object sender, EventArgs e){
        Debug.Log("not enough mana");
    }

    private void When_SkillIsAlreadyOn(object sender, EventArgs e){
        Debug.Log("skill is already on");
    }

    private void When_BuffDisappeared(object sender, Buffs.BuffDisappearedEventArgs e){
        Debug.Log(e.id + " disappeared");
    }
}
