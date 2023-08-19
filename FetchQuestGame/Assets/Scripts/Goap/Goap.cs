using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Goap : NetworkBehaviour
{
    public List<GoapAction> GoapActions;

    public GameObject goapActionKeeper;
    
    public float timeBetweenActions;
    public bool pausedGoap; // resume bool when each action is complete

    // set list of all actions, ordered by cost
    MeleeAttack meleeScript;
    RangedAttack rangedScript;
    void setListOfGoapActions()
    {
        meleeScript = goapActionKeeper.GetComponent<MeleeAttack>();
        GoapActions[0] = meleeScript;

        rangedScript = goapActionKeeper.GetComponent<RangedAttack>();
        GoapActions[1] = rangedScript;

    }

    void Start()
    {
        // assign goap actions list
        setListOfGoapActions();

        // start goap cycle
        // TO DO: make cmd
        StartCoroutine(RunState());
    }

    public void NextState()
    {
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
            if (GoapActions[i].CheckPreconditions())
            {
                RunAction(i);
            }
        }
        
        //yield return new WaitForSeconds(timeBetweenActions);
        yield return new WaitUntil(() => pausedGoap == false);
        StartCoroutine(RunState());
    }

    //[ClientRPC]
    float RunAction(int i)
    {
        GoapActions[i].RunAction();
        //isActing = true;
        //Make sure the same action is run across all clients
        return 0;
        
    }

}