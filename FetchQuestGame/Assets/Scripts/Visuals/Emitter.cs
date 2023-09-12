using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Emitter : MonoBehaviour
{
    public GameObject[] snowflakes;
    public float spawnDelay;

    bool snowing = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Snowing());
    }

    IEnumerator Snowing()
    {
        int index = Random.Range(0, snowflakes.Length);
        float randZ = Random.Range(-.3f, .3f);
        float randY = Random.Range(-.2f, -.05f);
        float randX = Random.Range(-.3f, .3f);
        GameObject nextSnowflake = snowflakes[index];
        nextSnowflake.GetComponent<Snowflake>().spawnerSetVelocity = new Vector3(randX, randY, randZ);

        float randScale = Random.Range(.25f, 1f);
        nextSnowflake.transform.localScale = new Vector3(1f, 1f, 1f);
        nextSnowflake.transform.localScale *= randScale;

        Instantiate(nextSnowflake, transform.position + new Vector3(randX*30f, 10f, randZ*30f), Quaternion.identity);

        yield return new WaitForSeconds(spawnDelay);

        if (snowing)
            StartCoroutine(Snowing());
    }

}
