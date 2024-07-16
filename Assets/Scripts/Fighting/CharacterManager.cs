 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    private NetworkGameManager networkGameManager;
    private NetworkPlayerManager networkPlayerManager;
    private NetworkPlayerManager enemyPlayerManager;
    private CharacterSelectMenu characterSelectMenu;

    private GameManager gameManager;

    [SerializeField]
    private CharacterSO[] characterData;
    public CharacterSO currentCharacter;
    public CharacterSO enemyCharacter;

    public CharacterSkills currentCharacterSkills;
    public CharacterSkills enemyCharacterSkills;
    public int characterIndex;
    private int characterDataLength;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
        characterIndex = 0;
        currentCharacter = characterData[characterIndex];
        characterDataLength = characterData.Length; 
        characterSelectMenu = FindObjectOfType<CharacterSelectMenu>();
        gameManager = GameManager.Singleton;
    }

    public void GetNetworkReference(NetworkGameManager script) {
        networkGameManager = script;
        Debug.LogWarning(networkGameManager);
    }

    public void GetPlayerManagerReference(NetworkPlayerManager script) {
        networkPlayerManager = script;
    }

    public void GetEnemyNetworkReference(NetworkPlayerManager script) {
        enemyPlayerManager = script;
    }

    public void ChangeCharacter(int character) {
        currentCharacter = characterData[character];
    }

    public void Initialize() {
        Debug.Log("Character Manager initialized");
        currentCharacterSkills = Instantiate(currentCharacter.characterSkills).GetComponent<CharacterSkills>();

        if (GameManager.GameCurrentMode == GameManager.GameType.Multiplayer) {
            enemyCharacter = characterData[enemyPlayerManager.network_syncCharacter.Value];
        } else {
            enemyCharacter = characterData[0];
        }
        
        enemyCharacterSkills = Instantiate(enemyCharacter.characterSkills).GetComponent<CharacterSkills>();

    }

    public void CharacterSelect(int next) {
        if (next == 1) {
            characterIndex ++;
        } else if (next == -1) {
            characterIndex --;
        }

        if (characterIndex >= characterDataLength) {
            characterIndex = 0;
        } else if (characterIndex < 0) {
            characterIndex = characterDataLength - 1;
        }

        currentCharacter = characterData[characterIndex];
        characterSelectMenu.UpdateCharacterText();
        networkPlayerManager.UpdateCharacter(characterIndex);
    }
    



     
}
