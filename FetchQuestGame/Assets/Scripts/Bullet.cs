using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        rigidbody.AddForce(camera.transform.forward * projectileSpeed);
        transform.Rotate(camera.transform.forward * new Vector2(90f, 0f));
        Destroy(gameObject, 20f);
    }

    void OnTriggerEnter(Collider collider)
    {
        // if collides with wall
        if (collider.gameObject.tag == "Wall")
        {
            print("hit");
            Destroy(gameObject);
        }
        // if collides with wall
        if (collider.gameObject.tag == "Enemy")
        {
            print("kill");
            Destroy(collider.gameObject);
        }
    }
}