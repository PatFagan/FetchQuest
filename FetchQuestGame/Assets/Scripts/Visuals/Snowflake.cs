using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowflake : MonoBehaviour
{
    public Vector3 spawnerSetVelocity;

    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = spawnerSetVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        print(gameObject.GetComponent<Rigidbody>().velocity);

    }

    void OnTriggerEnter(Collider collider)
    {
        // if player stands on platform, trigger sinking
        if (collider.gameObject.tag == "Ground")
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
