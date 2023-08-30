using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : GoapAction
{
    //public Collider handCollider;
    public GameObject[] attack;

    // action constructor, create necessary preconditions
    public MeleeAttack()
    {
        preconditions.Add("CloseToPlayer", false);
    }

    public override bool CheckPreconditions()
    {
        if (preconditions["CloseToPlayer"] == true)
            return true;
        else
            return false;
    }

    void Update()
    {
        preconditions["CloseToPlayer"] = goapAgentScript.closeToPlayer;
    }

    public override float RunAction()
    {
        print("melee, cost: " + cost);
        
        SetTarget(currentTarget);

        // pick a random ranged attack
        print(target.name);
        GameObject newAttack = attack[0];
        newAttack.GetComponent<EnemyProjectile>().playerTarget = target;
        newAttack.GetComponent<StickToPlayer>().parentPlayer = gameObject.transform;
        Instantiate(attack[0], transform.position, Quaternion.identity);

        return runTimeInSeconds;
    }

    /*
    public override float RunAction()
    {
        print("melee, cost: " + cost);

        SetTarget(currentTarget);
        //goapAgentScript.GetComponent<Animator>().SetTrigger("Melee");
        //StartCoroutine(EnableHandDamage());
        //StartCoroutine(endRunning());
       
        return runTimeInSeconds;
    }

    IEnumerator EnableHandDamage()
    {
        print("enable hand damage");
        yield return new WaitForSeconds(1f); // wind up
        //photonView.RPC("activateHandDamage", PhotonTargets.All, true); // enable damage
        yield return new WaitForSeconds(2f); // swing
        //photonView.RPC("activateHandDamage", PhotonTargets.All, false); // disable damage
    }

    //[PunRPC]
    public void activateHandDamage(bool damaging)
    {
        handCollider.enabled = damaging; // enable damage
    }
    */
}