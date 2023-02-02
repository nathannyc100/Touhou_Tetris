using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyReloader : MonoBehaviour
{
    void Awake(){
        DependencyManager.instance.ReloadDependency();
    }
}
