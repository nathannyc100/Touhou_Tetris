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

    public int[] skillCD;
    private int buffTagCount;
    private CharacterData.SkillName skillTagCount;
    private int blockRemoveCount;

    // Final skill variables
    private int skillFinalInitialCooldown;
    private bool skillFinalNoInitialCooldown = true;
    private int skillFinalRegularCooldown = 60;
    private bool finalSkillReusable = false;
    private bool finalSkillUsed = false;

    // Event Handlers
    public event EventHandler<AddBuffsEventArgs> AddBuffs;
    public event EventHandler<HealEventArgs> Heal;
    public event EventHandler<DamageEventArgs> Damage;
    public event EventHandler NotEnoughMana;
    public event EventHandler<SkillBindedEventArgs> SpellBinded;
    public event EventHandler<DecreaseManaEventArgs> DecreaseMana;
    // public event EventHandler SkillIsAlreadyOn;
    public event EventHandler<SkillTriggeredEventArgs> SkillTriggered;
    public event EventHandler FinalSkillStillLocked;
    public event EventHandler FinalSkillAlreadyUsed;

    public class AddBuffsEventArgs : EventArgs {
        public CharacterData.BuffName id;
        public CharacterData.SkillName skillTag;
        public int buffTag;
        public float buffAmount;
        public int duration;
        public int selecterValue;
        public CharacterData.Color color;
        public CharacterData.Keypress key;
        public string other;
    }

    public class HealEventArgs : EventArgs {
        public int amount;
    }

    public class DamageEventArgs : EventArgs {
        public int amount;
    }

    public class SkillBindedEventArgs : EventArgs {
        public CharacterData.SkillName id;
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
        skillCD = new int[] { 0, 0, 0, 0, 0, 0 };

        if (skillFinalNoInitialCooldown){
            skillFinalInitialCooldown = 0;
        } else {
            skillFinalInitialCooldown = skillFinalRegularCooldown;
        }

        finalSkillUsed = false;
    }

    private void When_TimeIncrement(object sender, Timing.TimeIncrementEventArgs e){
        for (int i = 0; i < skillCD.Length; i ++){
            if (skillCD[i] > 0){
                skillCD[i] -= (int)timing.increment;
            }
        }

        if (skillFinalInitialCooldown > 0){
            skillFinalInitialCooldown --;
        }
    }

    private void When_OnSkillPressed(object sender, Piece.OnSkillPressedEventArgs e){
        int character = board.character;
        int manaCost = CharacterData.skillData[character, (int)e.id].manaCost;

        // check if skill is already on, used if skill can't be proced when on uptime
        //
        // if (skillCD[(int)e.id] != 0){
        //     SkillIsAlreadyOn?.Invoke(this, EventArgs.Empty);
        //     return;
        // }

        if (skillCD[(int)e.id] > 0){
            Debug.Log("Skill in cd: " + skillCD[(int)e.id]);
            return;
        } 

        if (skillFinalInitialCooldown > 0 && e.id == CharacterData.SkillName.SkillFinal){
            FinalSkillStillLocked?.Invoke(this, EventArgs.Empty);
            return;
        }

        if (mana.manaCount < manaCost){
            NotEnoughMana?.Invoke(this, EventArgs.Empty);
            return;
        }

        if (buffs.totalBuffs.SpellBind[(int)e.id] == true || buffs.totalBuffs.SpellBindAll == true){
            SpellBinded?.Invoke(this, new SkillBindedEventArgs { id = e.id } );
            return;
        }

        if (e.id == CharacterData.SkillName.SkillFinal && finalSkillUsed){
            FinalSkillAlreadyUsed?.Invoke(this, EventArgs.Empty);
            return;
        }

        if (e.id == CharacterData.SkillName.SkillFinal && finalSkillReusable == false){
            finalSkillUsed = true;
        }

        DecreaseMana?.Invoke(this, new DecreaseManaEventArgs { amount = manaCost } );
        TriggerSkill(e.id);

    }

    private void TriggerSkill(CharacterData.SkillName id) {
        buffTagCount = 0;
        skillTagCount = id;

        blockRemoveCount = 0;

        // Set skill cd if there is one
        if (CharacterData.skillData[board.character, (int)id].CD != 0){
            skillCD[(int)id] = CharacterData.skillData[board.character, (int)id].CD;
        }

        foreach (CharacterData.SkillConstruct skillConstruct in CharacterData.skillData[board.character, (int)id].construct){

            switch (skillConstruct.id)
            {
                case CharacterData.ConstructName.BanKeypress :
                    BanKeypress(skillConstruct.keypress, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.BlockColorAttackBuff :
                    BlockColorAttackBuff(skillConstruct.blockColor, skillConstruct.amount, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.ChangeAllColorPercentage :
                    ChangeAllColorPercentage(skillConstruct.blockColor, skillConstruct.amount, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.ChangeBlockColor :
                    ChangeBlockColor();
                    break;
                case CharacterData.ConstructName.ChangeManaRegen :
                    ChangeManaRegen(skillConstruct.amount, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.ChangeSelfColorPercentage :
                    ChangeSelfColorPercentage(skillConstruct.amount, skillConstruct.duration, skillConstruct.blockColor);
                    break;
                case CharacterData.ConstructName.DamageChanging :
                    DamageChanging(skillConstruct.amount, skillConstruct.dependentValue);
                    break;
                case CharacterData.ConstructName.DamageFixed :
                    DamageFixed(skillConstruct.amount);
                    break;

                case CharacterData.ConstructName.EnemyBuffMultiplier :
                case CharacterData.ConstructName.EnemyBuffPercentage :
                case CharacterData.ConstructName.EnemyDebuffMultiplier :
                case CharacterData.ConstructName.EnemyDebuffPercentage :

                case CharacterData.ConstructName.HealChangeing :
                    HealChangeing(skillConstruct.amount, skillConstruct.dependentValue);
                    break;
                case CharacterData.ConstructName.HealFixed :
                    HealFixed(skillConstruct.amount);
                    break;
                case CharacterData.ConstructName.Invisibility :
                    Invisibility(skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.RemoveEnemyBlocks :
                case CharacterData.ConstructName.RemoveSelfBlocks :

                case CharacterData.ConstructName.SelfBuffMultiplier :
                    SelfBuffMultiplier(skillConstruct.amount, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.SelfBuffPercentace :
                case CharacterData.ConstructName.SelfDebuffMultiplier :
                case CharacterData.ConstructName.SelfDebuffPercentage :
                case CharacterData.ConstructName.SkillBuff :
                    SkillBuff(skillConstruct.amount, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.SpellBind :
                    SpellBind(skillConstruct.amount, skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.SpellBindAll :
                    SpellBindAll(skillConstruct.duration);
                    break;
                
                case CharacterData.ConstructName.StealHold :
                case CharacterData.ConstructName.StopClearing :
                    StopClearing(skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.StopRegularAttack :
                    StopRegularAttack(skillConstruct.duration);
                    break;
                case CharacterData.ConstructName.StopTime :
                case CharacterData.ConstructName.TrapBlocks :
                case CharacterData.ConstructName.Weaken :
                default: 
                    break;
            }

            buffTagCount ++;
        }

        SkillTriggered?.Invoke(this, new SkillTriggeredEventArgs { id = id } );
    }

    private void BanKeypress(CharacterData.Keypress key, int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.BanKeypress, skillTag = skillTagCount, buffTag = buffTagCount, key = key, duration = duration } );
    }

    private void BlockColorAttackBuff(CharacterData.Color color, float amount, int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.BlockColorAttackBuff, skillTag = skillTagCount, buffTag = buffTagCount, color = color, buffAmount = amount, duration = duration } );
    }

    private void ChangeAllColorPercentage(CharacterData.Color color, float amount, int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.ChangeAllColorPercentage, skillTag = skillTagCount, buffTag = buffTagCount, buffAmount = amount, duration = duration } );
    }

    private void ChangeBlockColor(){

    }

    private void ChangeManaRegen(int regenMultiplier, int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.ChangeManaRegen, skillTag = skillTagCount, buffTag = buffTagCount, buffAmount = regenMultiplier, duration = duration } );
    }

    private void ChangeSelfColorPercentage(float changePercentage, int duration, CharacterData.Color color){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.ChangeAllColorPercentage, skillTag = skillTagCount, buffTag = buffTagCount, buffAmount = changePercentage, color = color, duration = duration } );
    }

    private void DamageChanging(int amount, CharacterData.DependentValues dependentValues){
        int finalDamage = 0;

        // Needs work

        if (dependentValues == CharacterData.DependentValues.BlocksRemoved){
            finalDamage = blockRemoveCount * amount;
        }

        Damage?.Invoke(this, new DamageEventArgs { amount = finalDamage } );
    }

    private void DamageFixed(int amount){
        Damage?.Invoke(this, new DamageEventArgs { amount = amount } );
    }

    private void HealChangeing(int amount, CharacterData.DependentValues dependentValues){
        int finalHeal = 0;

        // Needs work

        Heal?.Invoke(this, new HealEventArgs { amount = finalHeal } );
    }

    private void HealFixed(int amount){
        Heal?.Invoke(this, new HealEventArgs { amount = amount } );
    }

    private void Invisibility(int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.Invisibility, skillTag = skillTagCount, buffTag = buffTagCount, duration = duration } );
    }

    private void SkillBuff(int amount, int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.SkillBuff, skillTag  = skillTagCount, buffTag = buffTagCount, buffAmount = amount, duration = duration } );
    }

    private void SpellBind(int amountOfSpells, int duration){
        int selecterValue = 0;
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.SpellBind, skillTag = skillTagCount, buffTag = buffTagCount, buffAmount = amountOfSpells, selecterValue = selecterValue, duration = duration } );
    }

    private void SpellBindAll(int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.SpellBindAll, skillTag = skillTagCount, buffTag = buffTagCount, duration = duration } );
    }

    private void StopClearing(int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.StopClearing, duration = duration } );
    }

    private void SelfBuffMultiplier(int buffMultiplier, int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.SelfBuffMultiplier, skillTag = skillTagCount, buffTag = buffTagCount, buffAmount = buffMultiplier, duration = duration } );
    }

    private void StopRegularAttack(int duration){
        AddBuffs?.Invoke(this, new AddBuffsEventArgs { id = CharacterData.BuffName.StopRegularAttack, skillTag = skillTagCount, buffTag = buffTagCount, duration = duration } );
    }










}
