using UnityEngine;
using System;

public class Skills : MonoBehaviour {

    [SerializeField]
    private Board board;
    [SerializeField]
    private Mana mana;
    [SerializeField]
    private Buffs buffs;
    [SerializeField]
    private Piece piece;
    [SerializeField]
    private Timing timing;

    public int[] skillOn;
    private int buffTagCount;
    private bool skillFinalAvailable;
    private CharacterData.SkillName skillTagCount;

    public event EventHandler<AddBuffsEventArgs> AddBuffs;
    public event EventHandler<HealEventArgs> Heal;
    public event EventHandler<DamageEventArgs> Damage;
    public event EventHandler NotEnoughMana;
    public event EventHandler SpellBinded;
    public event EventHandler<DecreaseManaEventArgs> DecreaseMana;
    // public event EventHandler SkillIsAlreadyOn;
    public event EventHandler<SkillTriggeredEventArgs> SkillTriggered;
    public event EventHandler FinalSkillStillLocked;

    public class AddBuffsEventArgs : EventArgs {
        public CharacterData.BuffName id;
        public CharacterData.SkillName skillTag;
        public int buffTag;
        public float buffAmount;
        public int duration;
        public int selecterValue;
        public CharacterData.Color color;
        public CharacterData.Keypress key;
    }

    public class HealEventArgs : EventArgs {
        public int amount;
    }

    public class DamageEventArgs : EventArgs {
        public int amount;
    }

    public class DecreaseManaEventArgs : EventArgs {
        public int amount;
    }

    public class SkillTriggeredEventArgs : EventArgs {
        public CharacterData.SkillName id;
    }

    private void OnEnable(){
        piece.OnSkillPressed += When_OnSkillPressed;
        board.ResetGame += When_ResetGame_InitializeSkills;
        timing.TimeIncrement += When_TimeIncrement;
    }

    private void When_ResetGame_InitializeSkills(object sender, EventArgs e){
        skillOn = new int[] { 0, 0, 0, 0, 0, 0 };
        skillFinalAvailable = false;
    }

    private void When_TimeIncrement(object sender, Timing.TimeIncrementEventArgs e){
        for (int i = 0; i < skillOn.Length; i ++){
            if (skillOn[i] > 0){
                skillOn[i] -= (int)timing.increment;
            }
        }

        if(e.time > 60){
            skillFinalAvailable = true;
        }
    }

    private void When_OnSkillPressed(object sender, Piece.OnSkillPressedEventArgs e){
        int character = board.character;
        int manaCost = CharacterData.skillData[character - 1, (int)e.id].manaCost;

        // check if skill is already on, used if skill can't be proced when on uptime
        //
        // if (skillOn[(int)e.id] != 0){
        //     SkillIsAlreadyOn?.Invoke(this, EventArgs.Empty);
        //     return;
        // }

        if (skillFinalAvailable == false && e.id == CharacterData.SkillName.SkillFinal){
            FinalSkillStillLocked?.Invoke(this, EventArgs.Empty);
            return;
        }

        if (mana.manaCount < manaCost){
            NotEnoughMana?.Invoke(this, EventArgs.Empty);
            return;
        }

        if (buffs.totalBuffs.SpellBind[(int)e.id] == true || buffs.totalBuffs.SpellBindAll == true){
            SpellBinded?.Invoke(this, EventArgs.Empty);
            return;
        }


        DecreaseMana?.Invoke(this, new DecreaseManaEventArgs { amount = manaCost } );
        TriggerSkill(e.id);

    }

    private void TriggerSkill(CharacterData.SkillName id) {
        buffTagCount = 0;
        skillTagCount = id;

        foreach (CharacterData.SkillConstruct skillConstruct in CharacterData.skillData[board.character - 1, (int)id].construct){

            switch (skillConstruct.id)
            {
                case CharacterData.ConstructName.ChangeManaRegen :
                    ChangeManaRegen(skillConstruct.amount, skillConstruct.duration);
                    AddToSkillOn(id, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.ChangeSelfColorPercentage :
                    ChangeSelfColorPercentage(skillConstruct.amount, skillConstruct.duration, skillConstruct.blockColor);
                    AddToSkillOn(id, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.DamageFixed :
                    DamageFixed(skillConstruct.amount);
                    break;
                case CharacterData.ConstructName.HealFixed :
                    HealFixed(skillConstruct.amount);
                    break;
                case CharacterData.ConstructName.SpellBind :
                    SpellBind(skillConstruct.amount, skillConstruct.duration);
                    AddToSkillOn(id, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.SpellBindAll :
                    SpellBindAll(skillConstruct.duration);
                    AddToSkillOn(id, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.SelfBuffMultiplier :
                    SelfBuffMultiplier(skillConstruct.amount, skillConstruct.duration);
                    AddToSkillOn(id, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.StopRegularAttack :
                    StopRegularAttack(skillConstruct.duration);
                    AddToSkillOn(id, skillConstruct.duration);
                    break;
                default: 
                    break;
            }

            buffTagCount ++;
        }

        SkillTriggered?.Invoke(this, new SkillTriggeredEventArgs { id = id } );
    }

    private void ChangeManaRegen(int regenMultiplier, int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.ChangeManaRegen, skillTag = skillTagCount, buffTag = buffTagCount, buffAmount = regenMultiplier, duration = duration } );
    }

    private void ChangeSelfColorPercentage(float changePercentage, int duration, CharacterData.Color color){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.ChangeAllColorPercentage, skillTag = skillTagCount, buffTag = buffTagCount, buffAmount = changePercentage, color = color, duration = duration } );
    }

    private void DamageFixed(int amount){
        Damage?.Invoke(this, new DamageEventArgs { amount = amount } );
    }

    private void HealFixed(int amount){
        Heal?.Invoke(this, new HealEventArgs { amount = amount } );
    }

    private void SpellBind(int amountOfSpells, int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.SpellBind, skillTag = skillTagCount, buffTag = buffTagCount, buffAmount = amountOfSpells, duration = duration } );
    }

    private void SpellBindAll(int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.SpellBindAll, skillTag = skillTagCount, buffTag = buffTagCount, duration = duration } );
    }

    private void SelfBuffMultiplier(int buffMultiplier, int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.SelfBuffMultiplier, skillTag = skillTagCount, buffTag = buffTagCount, buffAmount = buffMultiplier, duration = duration } );
    }

    private void StopRegularAttack(int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.StopRegularAttack, skillTag = skillTagCount, buffTag = buffTagCount, duration = duration } );
    }

    private void AddToSkillOn(CharacterData.SkillName skillName, int duration){
        skillOn[(int)skillName] = duration;
    }









}
