using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Text : MonoBehaviour
{
    private Board board;
    private Health health;
    private Mana mana;
    private GameManager gameManager;
    private NetworkManagerScript networkManagerScript;

    [SerializeField]
    private TextMeshProUGUI characterName;
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private TextMeshProUGUI manaText;

    [SerializeField]
    private TextMeshProUGUI enemyCharacter;
    [SerializeField]
    private TextMeshProUGUI enemyHealth;
    [SerializeField]
    private TextMeshProUGUI enemyMana;

    [SerializeField]
    private GameObject multiplayerUI;


    public int num = 0;

    private void Awake(){
        this.board = FindObjectOfType<Board>();
        this.health = FindObjectOfType<Health>();
        this.mana = FindObjectOfType<Mana>();
        this.networkManagerScript = FindObjectOfType<NetworkManagerScript>();
        this.gameManager = GameManager.instance;
    }

    private void OnEnable(){
        health.HealthChanged += When_HealthChanged;
        mana.ManaChanged += When_ManaChanged; 

        switch (GameManager.GameCurrentMode){
            case GameManager.GameType.Singleplayer :
                multiplayerUI.SetActive(false);
                break;
            case GameManager.GameType.Multiplayer :
                EnableMultiplayer();

                multiplayerUI.SetActive(true);
                break;
            default :
                break;
        }
    }

    private void OnDisable(){
        health.HealthChanged -= When_HealthChanged;
        mana.ManaChanged -= When_ManaChanged; 
    }

    private void Start(){
        characterName.text = CharacterData.Characters[board.character].name;
    }

    private void EnableMultiplayer(){
        networkManagerScript.EnemyCharacterChanged += When_EnemyCharacterChanged;
        networkManagerScript.EnemyHealthChanged += When_EnemyHealthChanged;
        networkManagerScript.EnemyManaChanged += When_EnemyManaChanged;
        networkManagerScript.EnemyHoldChanged += When_EnemyHoldChanged;
    }

    private void When_HealthChanged(object sender, Health.HealthChangedEventArgs e){
        healthText.text = e.health.ToString();
    }

    private void When_ManaChanged(object sender, Mana.ManaChangedEventArgs e){
        manaText.text = e.mana.ToString();
    }

    private void When_EnemyCharacterChanged(object sender, NetworkManagerScript.EnemyCharacterChangedEventArgs e){
        enemyCharacter.text = CharacterData.Characters[e.id].name;
    }

    private void When_EnemyHealthChanged(object sender, NetworkManagerScript.EnemyHealthChangedEventArgs e){
        enemyHealth.text = e.health.ToString();
    }

    private void When_EnemyManaChanged(object sender, NetworkManagerScript.EnemyManaChangedEventArgs e){
        enemyMana.text = e.mana.ToString();
    }

    private void When_EnemyHoldChanged(object sender, NetworkManagerScript.EnemyHoldChangedEventArgs e){

    }

}
