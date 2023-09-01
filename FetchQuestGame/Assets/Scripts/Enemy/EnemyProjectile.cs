using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject playerTarget;
    public float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider collider)
    {
        // if collides with wall
        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}