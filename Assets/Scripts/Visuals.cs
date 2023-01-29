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
        skills.FinalSkillStillLocked += When_FinalSkillStillLocked;
        skills.SpellBinded += When_SkillBinded;
        skills.FinalSkillAlreadyUsed += When_FinalSkillAlreadyused;
        // skills.SkillIsAlreadyOn += When_SkillIsAlreadyOn;
        buffs.BuffDisappeared += When_BuffDisappeared;
        health.DamageDelt += When_DamageDelt;
        health.RegularAttackStopped += When_RegularAttackStopped;
    }

    private void Start(){
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

    private void When_DamageDelt(object sender, Health.DamageDeltEventArgs e){
        DamagePopup.Create(new Vector3(10, 0, 0), e.damage);
        Debug.Log("Damage done");
    }

    private void When_RegularAttackStopped(object sender, EventArgs e){
        Debug.Log("Regular attack stopped");
    }

    private void When_FinalSkillStillLocked(object sender, EventArgs e){
        Debug.Log("Final skill still locked");
    }

    private void When_SkillBinded(object sender, Skills.SkillBindedEventArgs e){
        Debug.Log(e.id + " binded");
    }

    private void When_FinalSkillAlreadyused(object sender, EventArgs e){
        Debug.Log("Final skill already used");
    }
}
