using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Buffs : MonoBehaviour
{
    //class for handling all buffs in character
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

    //class for handling total buffs, eg sum of buffs
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
        public bool StopClearing;
        public bool StopRegularAttack;
        public bool StopTime;
    }

    [SerializeField]
    private Board board;
    [SerializeField]
    private Skills skills;
    [SerializeField]
    private Timing timing;
    private GameManager gameManager;

    private List<CharacterBuffs> characterBuffs;
    public TotalBuffs totalBuffs;
    private bool calculateTotaluffs;

    public event EventHandler<BuffDisappearedEventArgs> BuffDisappeared;

    public class BuffDisappearedEventArgs : EventArgs {
        public CharacterData.BuffName id;
    }

    private void OnEnable(){
        this.gameManager = GameManager.instance;

        gameManager.ResetGame += When_ResetGame_InitializeBuffs;
        skills.AddBuffs += When_AddBuffs;
        timing.TimeIncrement += When_TimeIncrement;
    }

    // reset buff values when game reset
    private void When_ResetGame_InitializeBuffs(object sender, EventArgs e){
        totalBuffs = new TotalBuffs {};
        characterBuffs = new List<CharacterBuffs>();
        calculateTotaluffs = false;
        InitializeBuffs();
    }

    // reset total buff values when game reset
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
        totalBuffs.SpellBind = new bool[] { false, false, false, false, false, false };
        totalBuffs.SpellBindAll = false;
        totalBuffs.SpellEffect = 1f;
        totalBuffs.StopClearing = false;
        totalBuffs.StopRegularAttack = false;
        totalBuffs.StopTime = false;
    }

    // function to add buff to list when event triggered, first check if there is the same buff already on the list 
    private void When_AddBuffs(object sender, Skills.AddBuffsEventArgs e){
        foreach (CharacterBuffs buffs in characterBuffs){
            if (buffs.skillTag == e.skillTag && buffs.buffTag == e.buffTag){
                buffs.currentTime = 0;
                return;
            }
        }

        AddBuffsToList(e);
    }

    // add buff to list
    private void AddBuffsToList(Skills.AddBuffsEventArgs e){
        characterBuffs.Add( new CharacterBuffs { id = e.id, skillTag = e.skillTag, buffTag = e.buffTag, buffAmount = e.buffAmount, selecterValue = e.selecterValue, color = e.color, key = e.key, duration = e.duration, currentTime = 0 } );
        RecalculateTotalBuffs();
    }

    // function to handle buff uptime everytime time ticks
    private void When_TimeIncrement(object sender, Timing.TimeIncrementEventArgs e){
        if (characterBuffs != null){
            foreach (CharacterBuffs buffs in characterBuffs){
                if (buffs.duration != 0){
                    buffs.currentTime += (int)timing.increment;
                }
            }

            // reiterate if buff is removed, or else the foreach will break
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
            calculateTotaluffs = false;
        }
    }

    // recalculate total buffs
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
                case CharacterData.BuffName.StopClearing :
                    totalBuffs.StopClearing = true;
                    break;
                case CharacterData.BuffName.StopRegularAttack :
                    totalBuffs.StopRegularAttack = true;
                    break;
                default:
                    break;
            }
        }
    }
}
