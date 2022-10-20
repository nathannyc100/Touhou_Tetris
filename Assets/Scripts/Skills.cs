using UnityEngine;

public class Skills : MonoBehaviour
{
    public Board board { get; private set; }

    public int[] hurt;
    public int[] bigHurt;
    public int[] hide;
    public int[] bleed;
    public int[] tarnish;
    public int[] resent;
    public int[] hunger;
    public int[] crudeOil;
    public int[] oil;
    public int[] magic;
    public int[] bloodThirst;
    public int[] poisoned;
    public int[] poisonSpell;
    public int[] bannedSpell;
    public int[] spellLock;
    public int[] hallucination;

    public float timer;



    private void hurtCalc(){
        if (Time.time - timer >= 1f){
            if (checkSkill(hurt)){
                this.board.health -= 5;
            }
            if (checkSkill(bigHurt)){
                this.board.health -= 10;
            }
        }
    }

    private bool checkSkill(int[] array){
        if (array[0] == 1){
            if (array[1] >= array[2]){
                array[1] = 0;
                return true;
            }
            else {
                array[1] ++;
            }
        }

        return false; 
    }

    public void OnSkill1(){
        if (this.board.skillPoints >= 1){
            
        }
    }

    public void OnSkill2(){

    }

    public void OnSkill3(){

    }

    public void OnSkill4(){

    }



    
}
