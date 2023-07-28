using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    Transform camera;
    Rigidbody rigidbody;
    public float projectileSpeed;
    WantedSystem wantedSystem;

    // Start is called before the first frame update
    void Start()
    {
        //wantedSystem = GameObject.FindGameObjectWithTag("WantedSystem").GetComponent<WantedSystem>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        transform.rotation = camera.transform.rotation;
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        rigidbody.velocity = transform.forward * projectileSpeed;
    }

    void OnTriggerEnter(Collider collider)
    {
        // if collides with wall
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        // if collides with wall
        if (collider.gameObject.tag == "Enemy")
        {
            // check if enemy is the target
            if (collider.gameObject.transform.parent.gameObject.GetComponent<Enemy>().enemyId == wantedSystem.currentTarget)
            {
                print("correct target");
                wantedSystem.wantedText.text = "KILLED";
                wantedSystem.kills++;
            }
            // if enemy is not the target
            else if (collider.gameObject.transform.parent.gameObject.GetComponent<Enemy>().enemyId != wantedSystem.currentTarget)
            {
                wantedSystem.casualties++;
            }

            // kill enemy
            Destroy(collider.gameObject);
        }
    }
}