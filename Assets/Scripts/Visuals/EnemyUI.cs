using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
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

    private string maxHealth;

    private void Awake() {
        characterManager = FindObjectOfType<CharacterManager>();
    }

    public void GetNetworkDependencies(NetworkPlayerManager script) {
        networkPlayerManager = script;

        networkPlayerManager.Server_SyncPlayer += When_Server_SyncPlayer;
        characterName.text = characterManager.enemyCharacter.name;
        healthText.text = networkPlayerManager.network_syncHealth.Value.ToString();
        manaText.text = networkPlayerManager.network_syncMana.Value.ToString();

        maxHealth = "/" + characterManager.enemyCharacter.characterHealth.ToString();
        slider.maxValue = characterManager.enemyCharacter.characterHealth;
        slider.value = slider.maxValue;
    }

    private void When_Server_SyncPlayer(object sender, NetworkPlayerManager.Server_SyncPlayerEventArgs e) {
        healthText.text = e.health.ToString() + maxHealth;
        manaText.text = e.mana.ToString();
        slider.value = e.health;
    }
}
