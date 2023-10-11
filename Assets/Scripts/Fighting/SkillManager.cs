using UnityEngine;
using System;
using TMPro;

public class SkillManager : MonoBehaviour
{
    private ControlsManager controlsManager;
    private CharacterSkills characterSkills;
    private CharacterSkills enemySkills;
    private CharacterManager characterManager;
    private CharacterSO characterSO;
    private Mana mana;
    private BuffManager buffManager;
    private NetworkPlayerManager networkPlayerManager;
    private NetworkPlayerManager networkEnemyManager;









    public event EventHandler<SkillTriggeredEventArgs> SkillTriggered;

    public class SkillTriggeredEventArgs : EventArgs {
        public int skill;
        public int time;
    }

    private void Awake() {
        controlsManager = FindObjectOfType<ControlsManager>();
        mana = FindObjectOfType<Mana>();
        buffManager = FindObjectOfType<BuffManager>();
        characterManager = FindObjectOfType<CharacterManager>();
        characterSO = characterManager.currentCharacter;
        characterSkills = characterManager.currentCharacterSkills;
        enemySkills = characterManager.enemyCharacterSkills;
    }

    private void OnEnable() {
        controlsManager.OnSkillPressed += When_OnSkillPressed;
    }

    private void OnDisable() {
        controlsManager.OnSkillPressed -= When_OnSkillPressed;
    }

    public void GetNetworkPlayer(NetworkPlayerManager script) {
        networkPlayerManager = script;
    }

    public void GetNetowrkEnemy(NetworkPlayerManager script) {
        networkEnemyManager = script;
    }

    private void When_OnSkillPressed(object sender, ControlsManager.OnSkillPressedEventArgs e) {
        int index = (int)e.id;
        if (e.id == CharacterData.SkillName.SkillFinal) {
            return;
        }

        if (characterSO.isPassive[index]) {
            return;
        }

        if (networkPlayerManager.network_skillCountdown[index] != 0) {
            return;
        }

        if (characterSO.skillMana[index] > networkPlayerManager.network_syncMana.Value) {
            Debug.Log("Not enough mana");
            return;
        }

        switch (index) {
            case 0:
                characterSkills.Skill1();
                break;
            case 1:
                characterSkills.Skill2();
                break;
            case 2:
                characterSkills.Skill3();
                break;
            case 3:
                characterSkills.Skill4();
                break;
            case 4:
                characterSkills.Skill5();
                break;
            default:
                break;
        }

        mana.ChangeManaFunction(-characterSO.skillMana[index]);
        SkillTriggered?.Invoke(this, new SkillTriggeredEventArgs { skill = index, time = characterSO.skillDuration[index] + characterSO.skillCD[index] });
        RecalculateSelfBuffs();
    }

    public void RecalculateSelfBuffs() {
        buffManager.ClearSelfBuffs();

        for (int i = 0; i < 5; i ++) {
            if (networkPlayerManager.network_skillCountdown[i] - characterSO.skillCD[i] > 0) {
                switch (i) {
                    case 0:
                        characterSkills.Skill1Buff(networkPlayerManager.network_skillCountdown[0] - characterSO.skillCD[0], true);
                        break;
                    case 1:
                        characterSkills.Skill2Buff(networkPlayerManager.network_skillCountdown[1] - characterSO.skillCD[1], true);
                        break;
                    case 2:
                        characterSkills.Skill3Buff(networkPlayerManager.network_skillCountdown[2] - characterSO.skillCD[2], true);
                        break;
                    case 3:
                        characterSkills.Skill4Buff(networkPlayerManager.network_skillCountdown[3] - characterSO.skillCD[3], true);
                        break;
                    case 4:
                        characterSkills.Skill5Buff(networkPlayerManager.network_skillCountdown[4] - characterSO.skillCD[4], true);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void RecalculateEnemyBuffs() {
        buffManager.ClearSelfBuffs();

        for (int i = 0; i < 5; i++) {
            if (networkEnemyManager.network_skillCountdown[i] - characterSO.skillCD[i] > 0) {
                switch (i) {
                    case 0:
                        enemySkills.Skill1Buff(networkPlayerManager.network_skillCountdown[0] - characterSO.skillCD[0], false);
                        break;
                    case 1:
                        enemySkills.Skill2Buff(networkPlayerManager.network_skillCountdown[1] - characterSO.skillCD[1], false);
                        break;
                    case 2:
                        enemySkills.Skill3Buff(networkPlayerManager.network_skillCountdown[2] - characterSO.skillCD[2], false);
                        break;
                    case 3:
                        enemySkills.Skill4Buff(networkPlayerManager.network_skillCountdown[3] - characterSO.skillCD[3], false);
                        break;
                    case 4:
                        enemySkills.Skill5Buff(networkPlayerManager.network_skillCountdown[4] - characterSO.skillCD[4], false);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

public class CharacterSkills : MonoBehaviour {
    public NetworkPlayerManager player;
    public NetworkPlayerManager enemy;
    public BuffManager buffManager;

    

    public virtual void InitializeSkill() { }


    public virtual void Skill1() { }
    public virtual void Skill1Buff(int duration, bool isSelfPlayer) { }
    public virtual void Skill2() { }
    public virtual void Skill2Buff(int duration, bool isSelfPlayer) { }
    public virtual void Skill3() { }
    public virtual void Skill3Buff(int duration, bool isSelfPlayer) { }
    public virtual void Skill4() { }
    public virtual void Skill4Buff(int duration, bool isSelfPlayer) { }
    public virtual void Skill5() { }
    public virtual void Skill5Buff(int duration, bool isSelfPlayer) { }
    public virtual void SkillFinal() { }
    public virtual void SkillFinalBuff(int duration, bool isSelfPlayer) { }
        
}

