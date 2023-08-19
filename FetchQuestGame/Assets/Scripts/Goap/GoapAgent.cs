using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapAgent : MonoBehaviour
{
    // current anim
    public string currentAnimation;

    // stats
    float distFromPlayer;

    public int currentTargetIndex;

    // bool
    public bool farFromPlayer;
    public bool closeToPlayer;

    public float closeToPlayerDist;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //if (gameObject.GetPhotonView().isMine)
        {
            currentTargetIndex = 1;
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // implement player aggro
        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
            distFromPlayer = Mathf.Sqrt(Mathf.Pow((player.transform.position.x - gameObject.transform.position.x), 2) + Mathf.Pow((player.transform.position.z - gameObject.transform.position.z), 2));

        //print("dist: " + distFromPlayer);

        // close check
        if (distFromPlayer <= closeToPlayerDist)
        {
            farFromPlayer = false;
            closeToPlayer = true;
        }
        else
        {
            farFromPlayer = true;
            closeToPlayer = false;
        }
    }

    public void SetTargetIndex(int newTarget)
    {
        currentTargetIndex = newTarget;
    }

}