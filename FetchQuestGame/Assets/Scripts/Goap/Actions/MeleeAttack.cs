using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : GoapAction
{
    public Collider handCollider;

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
        //if (PhotonNetwork.isMasterClient)
        {
            if (running)
            {
                goapAgentScript.GetComponent<Animator>().SetTrigger("Melee");
                StartCoroutine(EnableHandDamage());
                StartCoroutine(endRunning());
            }
            preconditions["CloseToPlayer"] = goapAgentScript.closeToPlayer;
        }
    }

    public override float RunAction()
    {
        print("run action");
        running = true;
       
        SetTarget(currentTarget);
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
}