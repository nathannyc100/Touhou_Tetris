using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Text : MonoBehaviour
{
    [SerializeField]
    private Board board;
    [SerializeField]
    private Health health;
    [SerializeField]
    private Mana mana;

    [SerializeField]
    private TextMeshProUGUI characterName;
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private TextMeshProUGUI manaText;


    public int num = 0;

    private void OnEnable(){
        health.HealthChanged += When_HealthChanged;
        mana.ManaChanged += When_ManaChanged;
        
    }

    private void Start(){
        characterName.text = CharacterData.Characters[board.character].name;
    }

    private void When_HealthChanged(object sender, Health.HealthChangedEventArgs e){
        healthText.text = e.health.ToString();
    }

    private void When_ManaChanged(object sender, Mana.ManaChangedEventArgs e){
        manaText.text = e.mana.ToString();
    }

}
