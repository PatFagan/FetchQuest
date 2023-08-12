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
            // subtract health
            health--;

            // knockback
            rigidbody.AddForce(collision.gameObject.GetComponent<Rigidbody>().velocity / 2f);

            // flash
            StartCoroutine(Flash());

            // if armor piercing rounds is false
            Destroy(collision.gameObject);

            // check if dead
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Flash()
    {
        Material mat = gameObject.GetComponent<MeshRenderer>().material;
        Color baseColor = Color.red;
        Color white = Color.white;

        mat.SetColor("_EmissionColor", white);

        // flash when hit
        //bossMesh.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(.1f);

        mat.SetColor("_EmissionColor", baseColor);
        //bossMesh.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }
}