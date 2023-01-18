using UnityEngine;

public class Skills : MonoBehaviour {

    public int damage { get; private set; }
    public int health { get; private set; }
    public Board board { get; private set; }
    public int character { get; private set; }
    public int skillPoints = 0;
 
    
    public Data.Skills[] skills = new Data.Skills[10];

    public void DamageCalc(int character, int[] color){
        damage = 0;
        float attack = Data.Characters[character].attack;
        float[] multiplier = Data.Characters[character].multiplier;

        for (int i = 0; i < 5; i ++){
            damage += Mathf.CeilToInt(attack * color[i] * multiplier[i]);
        }
        
        health -= damage;
    }

    public void SkillUpdate(){
        character = this.board.character;
        for (int i = 0; i < skills.Length; i ++){
            switch (this.skills[i].type) {
                case 1: 


                
                
                default: break;
            }
        }
    }

    public void Hurt(int stacks){
        this.board.health -= 5;
    }

    public void BigHurt(int stacks){
        this.board.health -= 5;
    }

    public void Bleed(int stacks, int character){
        int amount = Mathf.RoundToInt(Data.Characters[character].health * stacks * 0.01f);
        this.board.health -= amount;
    }

    public void Burn(int stacks){

    }

    public void Regen(){
        int health = this.board.health + 50;
        if (health > Data.Characters[character].health){
            this.board.health = Data.Characters[character].health;
        } else {
            this.board.health = health;
        }
    }

    public void Drunk(){

    }

    public void DrunkAF(){

    }

    public void Blind(){

    }

    public void Poison(int stacks){
        int temp;
        switch (stacks)
        {
            case 1:
                temp = 3;
                break;
            case 2:
                temp = 5;
                break;
            case 3:
                temp = 10;
                break;
            default:
                temp = 0;
                break;
        }
        this.board.health -= temp;
    }

    public void PoisonSpell(){
        this.board.health -= 5;
    }

    public void Spell(){
        this.board.health -= 2;
    }

    public void SpellBind(){

    }

    public void Berserk(){

    }

    public void Hallucination(){

    }

    public void ExtremePoison(int character){
        int amount = Mathf.RoundToInt(Data.Characters[character].health * 0.01f);
        this.board.health -= amount;
    }

    public void Venom(){
        this.board.health -= 1;
    }

    public void Chaos(){

    }

    public void AntiHeal(){

    }








}
