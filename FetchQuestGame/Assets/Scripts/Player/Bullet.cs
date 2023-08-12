using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    Transform camera;
    Rigidbody rigidbody;
    public float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}