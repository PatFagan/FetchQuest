using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    Transform player;
    Vector3 faceDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // switch players based on distance and attacking
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        faceDirection = transform.position - player.position;
        transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(new Vector3(faceDirection.x, 0f, faceDirection.z)), Time.deltaTime * 40f);
    }
}