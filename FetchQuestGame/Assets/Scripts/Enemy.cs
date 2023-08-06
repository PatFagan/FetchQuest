using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //health--;
            print("hit");

            rigidbody.AddForce(collision.gameObject.GetComponent<Rigidbody>().velocity);

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}