using System;

public class ReimuHakuraSkills : CharacterSkills {
    private SkillManager skillManager;

    public override void InitializeSkill() {
        skillManager = FindObjectOfType<SkillManager>();

        skillManager.SkillTriggered += Skill1Passive;
    }

    private void Skill1Passive(object sender, EventArgs e) {
        player.HealServerRPC(500);
    }

    public override void Skill2Buff(int duration, bool isSelfPlayer) {
        Buff buff = new Buff{ type = BuffType.SpellBindAll, duration = duration };

        if (isSelfPlayer) {
            buffManager.AddSelfBuff(buff);
        } else {
            buffManager.AddEnemyBuff(buff);
        }
    }

    public override void Skill3Buff(int duration, bool isSelfPlayer) {
        Buff buff = new Buff { type = BuffType.ChangeSelfColorPercentage, duration = duration, array = new float[5] { 1, 1, 1, 1, 1 } };

        if (isSelfPlayer) {
            buffManager.AddSelfBuff(buff);
        } else {
            buffManager.AddEnemyBuff(buff);
        }
    }

    public override void Skill4() {
        enemy.DealDamageServerRPC(2000);
    }

    public override void Skill4Buff(int duration, bool isSelfPlayer) {

    }

    public override void Skill5() {
        enemy.DealDamageServerRPC(2000);
        enemy.ChangeManaServerRPC(-enemy.network_syncMana.Value);
    }

    public override void Skill5Buff(int duration, bool isSelfPlayer) {
        Buff buff = new Buff { type = BuffType.StopHeal, duration = duration };

        if (isSelfPlayer) {
            buffManager.AddSelfBuff(buff);
        }
        else {
            buffManager.AddEnemyBuff(buff);
        }
    }

    public override void SkillFinal() {
        
    }





}
