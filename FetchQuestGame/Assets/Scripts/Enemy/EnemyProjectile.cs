using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody rigidbody;
    public float projectileSpeed;
    public GameObject playerTarget;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        transform.rotation = playerTarget.transform.rotation;
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
    }
}