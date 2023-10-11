using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "ScriptableObjects/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public Image characterImage;
    public int characterHealth;
    public int characterAttack;
    public float[] colorMultiplier;

    public string characterText;

    public GameObject characterSkills;

    public int[] skillMana;
    public int[] skillDuration;
    public int[] skillCD;
    public bool[] isPassive;

}
