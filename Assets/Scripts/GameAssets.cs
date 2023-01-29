using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i {
        get {
            if (_i == null) _i = FindObjectOfType<GameAssets>();
            return _i;
        }
    }

    public Transform DamagePopup; 
}
