using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Emitter : MonoBehaviour
{
    public GameObject[] snowflakes;
    public float spawnDelay;

    bool spawnStart = false;
    bool snowing = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Snowing());
    }

    IEnumerator Snowing()
    {
        int index = Random.Range(0, snowflakes.Length);
        float randZ = Random.Range(-20, 20);
        float randX = Random.Range(-20, 20);
        GameObject nextSnowflake = snowflakes[index];
        nextSnowflake.GetComponent<Snowflake>().spawnerSetVelocity = new Vector3(randX, 0f, randZ);

        Instantiate(nextSnowflake, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(spawnDelay);

        if (snowing)
            StartCoroutine(Snowing());
    }

}
