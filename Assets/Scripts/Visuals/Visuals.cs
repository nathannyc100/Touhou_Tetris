using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Visuals : MonoBehaviour
{
    private Health health;
    private Mana mana;
    private Buffs buffs;

    private void Awake(){
        this.health = GetComponent<Health>();
        this.mana = GetComponent<Mana>();
        this.buffs = GetComponent<Buffs>();
    }

    private void OnEnable(){
        buffs.BuffDisappeared += When_BuffDisappeared;
        health.DamageDelt += When_DamageDelt;
        health.RegularAttackStopped += When_RegularAttackStopped;
    }

    private void OnDisable(){
        buffs.BuffDisappeared -= When_BuffDisappeared;
        health.DamageDelt -= When_DamageDelt;
        health.RegularAttackStopped -= When_RegularAttackStopped;
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


}
