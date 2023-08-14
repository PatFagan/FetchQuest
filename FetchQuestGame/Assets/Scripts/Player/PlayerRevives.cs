using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRevives : MonoBehaviour
{
    PlayerHealth playerHealthScript;

    void Start()
    {
        playerHealthScript = GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider collider)
    {
        // revive if you are dead and another player collides w you
        if (collider.gameObject.name == "Player" && playerHealthScript.health <= 0f)
        {
            playerHealthScript.health = playerHealthScript.maxHealth;
        }
    }
}