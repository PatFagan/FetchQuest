using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingAttack : GoapAction
{
    // have randomly choose a ranged attack from within this script, just use this as
    // an indicator that the next attack should be something ranged
    
    public GameObject[] attack;
    public int maxAttackIndex = 1;

    // action constructor, create necessary preconditions
    public SeekingAttack()
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
        preconditions["FarFromPlayer"] = goapAgentScript.farFromPlayer;

        /*
        if (running)
        {
            goapAgentScript.GetComponent<Animator>().SetTrigger("Ranged");
            StartCoroutine(endRunning());
        }
        */
    }

    public override float RunAction()
    {
        print("ranged, cost: " + cost);
        
        SetTarget(currentTarget);

        // pick a random ranged attack
        print(target.name);
        attack[0].GetComponent<EnemyProjectile>().playerTarget = target;
        Instantiate(attack[0], transform.position, Quaternion.identity);

        return runTimeInSeconds;
    }
    
}