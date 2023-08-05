using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyId;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        //enemyId = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            print("hit");

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}