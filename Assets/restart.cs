using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player is tagged as "Player"
        {
            RespawnManager respawnManager = FindObjectOfType<RespawnManager>();
            if (respawnManager != null)
            {
                

                respawnManager.RespawnPlayer();
            }
        }
    }
}
