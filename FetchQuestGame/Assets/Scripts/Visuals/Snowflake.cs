using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowflake : MonoBehaviour
{
    public Vector3 spawnerSetVelocity;

    void Start()
    {
        //gameObject.GetComponent<Rigidbody>().velocity = spawnerSetVelocity;
        //transform.Translate(spawnerSetVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(spawnerSetVelocity);

        // add random velocity bursts
        float randBurstChance = Random.Range(0f, 100f);
        if (randBurstChance < .2f)
        {
            float randZ = Random.Range(-.05f, .05f);
            float randY = Random.Range(-.2f, -.05f);
            float randX = Random.Range(-.05f, .05f);

            // if x and z are greater, decrease y
            //randY = randY / Mathf.Abs(randX + randZ);

            spawnerSetVelocity = new Vector3(randX, randY, randZ);
        }
    }
}
