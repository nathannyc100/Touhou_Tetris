 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    private NetworkGameManager networkGameManager;
    private NetworkPlayerManager networkPlayerManager;
    private NetworkPlayerManager enemyPlayerManager;
    private CharacterSelectMenu characterSelectMenu;

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
        currentCharacterSkills = Instantiate(currentCharacter.characterSkills).GetComponent<CharacterSkills>();

        enemyCharacter = characterData[enemyPlayerManager.network_syncCharacter.Value];
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
