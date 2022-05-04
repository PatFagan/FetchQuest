using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public float grapplingSpeed;
    public float projectileSpeed;
    public float lifespan;

    Rigidbody rigidbody;
    bool grapplingMovement;
    GameObject player;
    Camera camera;
    float dist;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        grapplingMovement = false;
        // get player script
        player = GameObject.Find("Player");
        rigidbody = GetComponent<Rigidbody>(); // get rigidbody
        StartCoroutine(DestroyAfterLifespan());

        // start velocity
        rigidbody.velocity = camera.transform.forward * projectileSpeed;
    }

    void OnTriggerEnter(Collider collider)
    {
        // if collides with wall
        if (collider.gameObject.tag == "Wall")
        {
            rigidbody.velocity = new Vector3(0, 0, 0); // stop hook movement
            grapplingMovement = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 playerPos = player.transform.position;
        dist = Mathf.Sqrt(Mathf.Pow(playerPos.x - transform.position.x, 2)
            + Mathf.Pow(playerPos.z - transform.position.z, 2));

        // if the player is pulled near the grappling hook, destroy it
        if (dist < 3 && grapplingMovement == true)
        {
            Destroy(gameObject);
        }
        if (grapplingMovement)
        {
            Vector3 directionToGrapple = Vector3.Normalize(transform.position - player.transform.position);

            player.transform.position =
                    Vector3.MoveTowards(
                        playerPos,
                        directionToGrapple,
                        grapplingSpeed * Time.fixedDeltaTime
                    );
        }
    }

    IEnumerator DestroyAfterLifespan()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}