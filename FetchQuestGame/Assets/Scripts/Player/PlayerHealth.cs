using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject deathUI;

    public Image healthBar;

    public bool invincible = false;

    void Update()
    {
        healthBar.fillAmount = health/maxHealth;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            print("player hit");
            
            if (!invincible)
                health--;

            if (health <= 0)
            {
                gameObject.GetComponent<PlayerMovement>().dead = true;
                deathUI.SetActive(true);
                print("death");
            }
        }
    }
}
