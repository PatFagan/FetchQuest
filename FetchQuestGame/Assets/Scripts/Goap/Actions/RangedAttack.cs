using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : GoapAction
{
    // have randomly choose a ranged attack from within this script, just use this as
    // an indicator that the next attack should be something ranged
    
    public GameObject[] attack;
    public GameObject[] spawnPositions;
    public int maxAttackIndex = 1;
    bool ending = false;

    public RangedAttack()
    {
        preconditions.Add("FarFromPlayer", false);
    }

    public override bool CheckPreconditions()
    {
        if (preconditions["FarFromPlayer"] == true)
            return true;
        else
            return false;
    }

    void Update()
    {
        //if (PhotonNetwork.isMasterClient)
        {
            preconditions["FarFromPlayer"] = goapAgentScript.farFromPlayer;
            if (running)
            {
                goapAgentScript.GetComponent<Animator>().SetTrigger("Ranged");
                StartCoroutine(endRunning());
            }
        }

    }

    public override float RunAction()
    {
        //print("ranged, cost: " + cost);

        // pick a random ranged attack
        //GameObject tmp = PhotonNetwork.Instantiate("Weapons/BossAttacks/" + 
            //attack[randomIndex].name, spawnPos.transform.position, Quaternion.identity, 0);

        SetTarget(currentTarget);
        return runTimeInSeconds;
    }
    
}