using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    /*
    public float grapplingSpeed;
    Rigidbody2D rigidbody;
    bool grapplingMovement;
    GameObject player;
    Player playerScript;

    void Start()
    {
        grapplingMovement = false;
        // get player script
        player = GameObject.Find("Player1");
        playerScript = player.GetComponent<Player>();
        rigidbody = GetComponent<Rigidbody2D>(); // get rigidbody
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // if collides with wall
        if (collider.gameObject.tag == "Wall")
        {
            rigidbody.velocity = new Vector2(0, 0); // stop hook movement
            grapplingMovement = true;
        }
    }

    void FixedUpdate()
    {
        Vector2 playerPos = player.transform.position;
        float dist = Mathf.Sqrt(Mathf.Pow(playerPos.x - transform.position.x, 2)
            + Mathf.Pow(playerPos.y - transform.position.y, 2));

        //Debug.Log(dist);
        if (dist < 3)
        {
            grapplingMovement = false;
        }
        if (grapplingMovement)
        {
            player.transform.position =
                    Vector2.MoveTowards(
                        playerPos,
                        transform.position,
                        grapplingSpeed * Time.fixedDeltaTime
                    );
        }
    }
    */
}