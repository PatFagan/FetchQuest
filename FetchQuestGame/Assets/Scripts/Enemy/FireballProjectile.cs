using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : EnemyProjectile
{
    Rigidbody rigidbody;
    GameObject localPlayerTarget;

    // Start is called before the first frame update
    void Start()
    {
        localPlayerTarget = gameObject.GetComponent<EnemyProjectile>().playerTarget;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        transform.rotation = localPlayerTarget.transform.rotation;
    }
    
    void Update()
    {
        rigidbody.velocity = -transform.forward * projectileSpeed;
    }
}
