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

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // subtract health
            health--;

            // knockback
            rigidbody.AddForce(collision.gameObject.GetComponent<Rigidbody>().velocity);

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

        // flash when hit
        mat.SetColor("_EmissionColor", white);

        yield return new WaitForSeconds(.1f);

        // return to original color
        mat.SetColor("_EmissionColor", baseColor);
    }
}