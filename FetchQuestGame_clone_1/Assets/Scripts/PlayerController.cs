using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    // movement variables
    float horizontal, vertical;
    public float moveSpeed;
    public Vector3 movement;
    Rigidbody rigidbody;

    void Start()
    {
        // set up rigidbody
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        movementManager();
    }

    void movementManager()
    {
        if (isLocalPlayer)
        {
            // movement
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            movement = new Vector3(horizontal, 0f, vertical);
            rigidbody.velocity = movement * moveSpeed;
        }
    }
}
