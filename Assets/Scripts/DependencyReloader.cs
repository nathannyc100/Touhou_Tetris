using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyReloader : MonoBehaviour
{
    private bool isFirstFrame = true;

    private void Awake(){
        DependencyManager.instance.ReloadDependency();
    }

    private void Update(){
        if (isFirstFrame){
            GameManager.instance.RunOnFirstFrame();
            isFirstFrame = false;
        }
    }


}
