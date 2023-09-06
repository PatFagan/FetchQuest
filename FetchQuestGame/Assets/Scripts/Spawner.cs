using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnedObjects;
    public float spawnDelay;

    public Image startCircle;
    public TMP_Text text;

    bool spawnStart = false;

    IEnumerator Spawning()
    {
        int index = Random.Range(0, spawnedObjects.Length);
        GameObject enemy = Instantiate(spawnedObjects[index], transform.position, transform.rotation);
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(Spawning());
    }

    void OnTriggerStay(Collider collider)
    {
        if (spawnStart == false)
        {
            // if player stands on platform, trigger sinking
            if (collider.gameObject.tag == "Player")
            {
                startCircle.gameObject.SetActive(true);
                text.gameObject.SetActive(true);

                if (Input.GetButton("Interact"))
                {
                    print("e");
                    startCircle.fillAmount += .01f;
                }
                if (Input.GetButtonUp("Interact"))
                {
                    startCircle.fillAmount = 0f;
                }

                if (startCircle.fillAmount == 1f)
                {
                    StartCoroutine(Spawning());
                    startCircle.fillAmount = 0f;
                    spawnStart = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        // if player stands on platform, trigger sinking
        if (collider.gameObject.tag == "Player")
        {
            startCircle.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
    }
}
