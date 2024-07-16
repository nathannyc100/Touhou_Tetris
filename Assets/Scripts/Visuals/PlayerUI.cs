using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System;

public class PlayerUI : MonoBehaviour
{
    private NetworkPlayerManager networkPlayerManager;
    private CharacterManager characterManager;

    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private TextMeshProUGUI manaText;
    [SerializeField]
    private TextMeshProUGUI characterName;
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private TextMeshProUGUI skill1Mana;
    [SerializeField]
    private TextMeshProUGUI skill1CD;
    [SerializeField]
    private TextMeshProUGUI skill2Mana;
    [SerializeField]
    private TextMeshProUGUI skill2CD;
    [SerializeField]
    private TextMeshProUGUI skill3Mana;
    [SerializeField]
    private TextMeshProUGUI skill3CD;
    [SerializeField]
    private TextMeshProUGUI skill4Mana;
    [SerializeField]
    private TextMeshProUGUI skill4CD;
    [SerializeField]
    private TextMeshProUGUI skill5Mana;
    [SerializeField]
    private TextMeshProUGUI skill5CD;

    private string maxHealth;

    private void Awake() {
        characterManager = FindObjectOfType<CharacterManager>();

    }

    public void GetNetworkDependencies(NetworkPlayerManager script) {
        networkPlayerManager = script;

        networkPlayerManager.Server_SyncPlayer += When_Server_SyncPlayer;
        networkPlayerManager.Server_SyncSkills += When_Server_SyncSkills;
        characterName.text = characterManager.currentCharacter.name;
        healthText.text = networkPlayerManager.network_syncHealth.Value.ToString();
        manaText.text = networkPlayerManager.network_syncMana.Value.ToString();

        Debug.Log(characterManager.enemyCharacter);
        maxHealth = "/" + characterManager.enemyCharacter.characterHealth.ToString();
        slider.maxValue = characterManager.enemyCharacter.characterHealth;
        slider.value = slider.maxValue;

        Debug.Log(characterManager.currentCharacter);

        if (characterManager.currentCharacter.skillMana[0] == 0) {
            skill1Mana.text = "";
        } else {
            skill1Mana.text = characterManager.currentCharacter.skillMana[0].ToString();
        }

        if (characterManager.currentCharacter.skillMana[1] == 0) {
            skill2Mana.text = "";
        } else {
            skill2Mana.text = characterManager.currentCharacter.skillMana[1].ToString();
        }

        if (characterManager.currentCharacter.skillMana[2] == 0) {
            skill3Mana.text = "";
        } else {
            skill3Mana.text = characterManager.currentCharacter.skillMana[2].ToString();
        }

        if (characterManager.currentCharacter.skillMana[3] == 0) {
            skill4Mana.text = "";
        } else {
            skill4Mana.text = characterManager.currentCharacter.skillMana[3].ToString();
        }

        if (characterManager.currentCharacter.skillMana[4] == 0) {
            skill5Mana.text = "";
        } else {
            skill5Mana.text = characterManager.currentCharacter.skillMana[4].ToString();
        }
 
        skill1CD.text = "";
        skill2CD.text = "";
        skill3CD.text = "";
        skill4CD.text = "";
        skill5CD.text = "";
    }

    private void When_Server_SyncPlayer(object sender, NetworkPlayerManager.Server_SyncPlayerEventArgs e) {
        healthText.text = e.health.ToString() + maxHealth;
        manaText.text = e.mana.ToString();
        slider.value = e.health;

    }

    private void When_Server_SyncSkills(object sender, NetworkPlayerManager.Server_SyncSkillsEventArgs e) {
        if (e.skillCD[0] == 0) {
            skill1CD.text = "";
        } else {
            skill1CD.text = e.skillCD[0].ToString();
        }

        if (e.skillCD[1] == 0) {
            skill2CD.text = "";
        } else {
            skill2CD.text = e.skillCD[1].ToString();
        }

        if (e.skillCD[2] == 0) {
            skill3CD.text = "";
        }
        else {
            skill3CD.text = e.skillCD[2].ToString();
        }

        if (e.skillCD[3] == 0) {
            skill4CD.text = "";
        } else {
            skill4CD.text = e.skillCD[3].ToString();
        }

        if (e.skillCD[4] == 0) {
            skill5CD.text = "";
        } else {
            skill5CD.text = e.skillCD[4].ToString();
        }

    }





}
 