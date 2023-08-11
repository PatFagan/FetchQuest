using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnedObjects;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        int index = Random.Range(0, spawnedObjects.Length);
        Instantiate(spawnedObjects[index]);
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(Spawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
