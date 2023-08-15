using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class Spawner : NetworkBehaviour
{
    public GameObject[] spawnedObjects;
    public float spawnDelay;

    public Image startCircle;
    public TMP_Text text;

    bool spawnStart = false;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Spawning());
        //NetworkIdentity.AssignClientAuthority(conn);
        //identity.AssignClientAuthority(conn);
    }

    IEnumerator Spawning()
    {
        CmdSpawn();
        //int index = Random.Range(0, spawnedObjects.Length);
        //Instantiate(spawnedObjects[index]);
        yield return new WaitForSeconds(spawnDelay);
        //StartCoroutine(Spawning());
    }

    //[Command]
    [Command(requiresAuthority = false)]
    void CmdSpawn()
    {
        RpcSpawn();
    }

    [ClientRpc]
    void RpcSpawn()
    {
        GameObject enemy = Instantiate(spawnedObjects[0], transform.position, transform.rotation);
        //NetworkServer.Spawn(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
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
