using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingProjectile : EnemyProjectile
{
    public float speed;
    private Transform target;
    public string targetTag;
    public float verticalOffset;

    float startingDist, currentDist;

    public bool arc;
    public bool playerAttack;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Transform>();

        startingDist = Mathf.Sqrt(Mathf.Pow((target.transform.position.x - gameObject.transform.position.x), 2) + Mathf.Pow((target.transform.position.z - gameObject.transform.position.z), 2));
    }

    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag(targetTag))
        {
            // get target
            target = GameObject.FindGameObjectWithTag(targetTag).GetComponent<Transform>();
            Vector3 trackingPos = new Vector3(target.position.x, verticalOffset, target.position.z);
            // move towards target
            transform.position = Vector3.MoveTowards(transform.position, trackingPos, speed * Time.fixedDeltaTime);
        }

        currentDist = Mathf.Sqrt(Mathf.Pow((target.transform.position.x - gameObject.transform.position.x), 2) + Mathf.Pow((target.transform.position.z - gameObject.transform.position.z), 2));

        if (arc)
            transform.Translate(new Vector3(0f, currentDist / 100f, 0f));
    }
}