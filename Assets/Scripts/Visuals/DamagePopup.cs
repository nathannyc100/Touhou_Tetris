using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    // Create damage popup
    public static DamagePopup Create(Vector3 position, int damageAmount){
        Transform damagePopupTransfrom = Instantiate(GameAssets.i.DamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransfrom.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    private static int sortingOrder;

    private const float Disappear_Timer_Max = 1f;

    private TextMeshPro damagePopupText;
    private float disappearTimer; 
    private Color textColor;

    private void Awake(){
        damagePopupText = transform.GetComponent<TextMeshPro>();
    }
    
    public void Setup(int damageAmount) {
        damagePopupText.SetText(damageAmount.ToString());
        textColor = damagePopupText.color;
        disappearTimer = Disappear_Timer_Max;

        sortingOrder ++;
        damagePopupText.sortingOrder = sortingOrder;
    }

    private void Update(){

        // Scale manager
        if (disappearTimer > Disappear_Timer_Max * 0.5f){
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } else {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        // Fading manager
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0){
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            damagePopupText.color = textColor;
            if (textColor.a < 0){
                Destroy(gameObject);
            }
        }

    }
}
