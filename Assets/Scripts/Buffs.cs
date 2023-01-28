using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Buffs : MonoBehaviour
{

    public class CharacterBuffs {
        public CharacterData.BuffName id;
        public CharacterData.SkillName skillTag;
        public int buffTag;
        public float buffAmount;
        public int duration;
        public int currentTime;
        public int selecterValue;
        public CharacterData.Color color;
        public CharacterData.Keypress key;
    }

    public class TotalBuffs {
        public bool[] SpellBind;
        public bool SpellBindAll;
        public float SelfDamageMultiplier;
        public float SelfDefenseMultiplier;
        public float[] BlockColorSpawnPercentage;
        public float[] BlockColorDamageBuff;
        public float HealBuff;
        public float ManaRegen;
        public bool[] KeypressBan;
        public bool ClearBan;
        public bool Invisibility;
        public float SpellEffect;
        public bool StopTime;
    }

    public class BuffTag {
        public int skill;
        public int buff;
    }

    [SerializeField]
    private Board board;
    [SerializeField]
    private Skills skills;
    [SerializeField]
    private Timing timing;

    private List<CharacterBuffs> characterBuffs;
    public TotalBuffs totalBuffs;
    private bool calculateTotaluffs;

    public event EventHandler<BuffDisappearedEventArgs> BuffDisappeared;

    public class BuffDisappearedEventArgs : EventArgs {
        public CharacterData.BuffName id;
    }

    private void Awake(){
        board.ResetGame += When_ResetGame_InitializeBuffs;
        skills.AddBuffs += When_AddBuffs;
        timing.TimeIncrement += When_TimeIncrement;
    }

    private void When_ResetGame_InitializeBuffs(object sender, EventArgs e){
        totalBuffs = new TotalBuffs {};
        characterBuffs = new List<CharacterBuffs>();
        calculateTotaluffs = false;
        InitializeBuffs();
    }

    private void InitializeBuffs(){
        totalBuffs.BlockColorDamageBuff = new float[] { 1f, 1f, 1f, 1f, 1f };
        totalBuffs.BlockColorSpawnPercentage = new float[] { 1f, 1f, 1f, 1f, 1f };
        totalBuffs.ClearBan = false;
        totalBuffs.HealBuff = 1f;
        totalBuffs.Invisibility = false;
        totalBuffs.KeypressBan = new bool[] { false, false, false, false, false };
        totalBuffs.ManaRegen = 1f;
        totalBuffs.SelfDamageMultiplier = 1f;
        totalBuffs.SelfDefenseMultiplier = 1f;
        totalBuffs.SpellBind = new bool[] { false, false, false, false, false };
        totalBuffs.SpellBindAll = false;
        totalBuffs.SpellEffect = 1f;
        totalBuffs.StopTime = false;
    }

    private void When_AddBuffs(object sender, Skills.AddBuffsEventArgs e){
        foreach (CharacterBuffs buffs in characterBuffs){
            if (buffs.skillTag == e.skillTag && buffs.buffTag == e.buffTag){
                buffs.currentTime = 0;
                return;
            }
        }

        AddBuffsToList(e);
    }

    private void AddBuffsToList(Skills.AddBuffsEventArgs e){
        characterBuffs.Add( new CharacterBuffs { id = e.id, skillTag = e.skillTag, buffTag = e.buffTag, buffAmount = e.buffAmount, selecterValue = e.selecterValue, color = e.color, key = e.key, duration = e.duration, currentTime = 0 } );
        RecalculateTotalBuffs();
    }

    private void When_TimeIncrement(object sender, Timing.TimeIncrementEventArgs e){
        if (characterBuffs != null){
            foreach (CharacterBuffs buffs in characterBuffs){
                if (buffs.duration != 0){
                    buffs.currentTime += (int)timing.increment;
                }

                Debug.Log(buffs.currentTime);
            }

            IterateAgain :
            foreach (CharacterBuffs buffs in characterBuffs){
                if (buffs.currentTime >= buffs.duration){
                    this.characterBuffs.Remove(buffs);
                    calculateTotaluffs = true;
                    BuffDisappeared?.Invoke(this, new BuffDisappearedEventArgs { id = buffs.id } );

                    goto IterateAgain;
                }
            }
        }

        if (calculateTotaluffs == true){
            RecalculateTotalBuffs();
        }
    }

    private void RecalculateTotalBuffs(){
        InitializeBuffs();

        float selfDamageMultiplier = 1f;
        float selfDamagePercentage = 1f;
        float SelfDefenseMultiplier = 1f;
        float selfDefensePercentage = 1f;


        foreach (CharacterBuffs buffs in characterBuffs){
            switch (buffs.id)
            {
                case CharacterData.BuffName.BanKeypress :
                    totalBuffs.KeypressBan[(int)buffs.key] = true;
                    break;
                case CharacterData.BuffName.BlockColorAttackBuff :
                    totalBuffs.BlockColorDamageBuff[(int)buffs.color] += buffs.buffAmount;
                    break;
                case CharacterData.BuffName.ChangeAllColorPercentage :
                    totalBuffs.BlockColorSpawnPercentage[(int)buffs.color] += buffs.buffAmount;
                    break;
                case CharacterData.BuffName.ChangeManaRegen :
                    totalBuffs.ManaRegen *= buffs.buffAmount;
                    break;
                case CharacterData.BuffName.SelfBuffMultiplier :
                    selfDamageMultiplier *= buffs.buffAmount;
                    break;
                case CharacterData.BuffName.SelfDebuffMultiplier :
                    selfDamageMultiplier *= buffs.buffAmount;
                    break;
                case CharacterData.BuffName.SpellBind :
                    totalBuffs.SpellBind[buffs.selecterValue] = true;
                    break;
                case CharacterData.BuffName.SpellBindAll :
                    totalBuffs.SpellBindAll = true;
                    break;
                case CharacterData.BuffName.StopRegularAttack :
                    selfDamageMultiplier = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
