using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class NetworkGameManager : NetworkBehaviour
{
    private GameManager gameManager;

    public NetworkVariable<float> gameTime = new NetworkVariable<float>(0);

    public override void OnNetworkSpawn(){
        if (!IsServer){
            gameManager = FindObjectOfType<GameManager>();
        }

        base.OnNetworkSpawn();
    }


}
