using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Goap : NetworkBehaviour
{
    public List<GoapAction> GoapActions; // list of relevant goap actions

    public GameObject goapActionKeeper; // linked object with all relevant goap action scripts

    float timeBetweenActions = 2f;

    // set list of all actions, ordered by cost
    MeleeAttack meleeScript;
    RangedAttack rangedScript;
    void setListOfGoapActions()
    {
        meleeScript = goapActionKeeper.GetComponent<MeleeAttack>();
        GoapActions[0] = meleeScript;

        rangedScript = goapActionKeeper.GetComponent<RangedAttack>();
        GoapActions[1] = rangedScript;

        print("goap actions list set");

    }

    void Start()
    {
        // assign goap actions list
        setListOfGoapActions();

        // start goap cycle
        // TO DO: make cmd
        StartCoroutine(RunState());
    }

    IEnumerator RunState()
    {
        //if (!PhotonNetwork.isMasterClient)
            //yield return null;

        // run through goap actions
        // run first action with all preconditions met
        for (int i = GoapActions.Count - 1; i >= 0; i--)
        {
            // found action with preconditions met
            if (GoapActions[i].CheckPreconditions())
            {
                print("found goap action to run");
                //RunAction(i);
                GoapActions[i].RunAction();
            }
        }
        
        // delay between actions
        yield return new WaitForSeconds(timeBetweenActions);

        // wait until action is complete to continue
        //yield return new WaitUntil(() => GoapActions[i].pausedGoap == false);
        StartCoroutine(RunState());
    }

    /*
    //[ClientRPC]
    float RunAction(int i)
    {
        GoapActions[i].RunAction();
        //isActing = true;
        //Make sure the same action is run across all clients
        return 0;
    }
    */

}