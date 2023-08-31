using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject deathUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            print("player hit");
            
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
