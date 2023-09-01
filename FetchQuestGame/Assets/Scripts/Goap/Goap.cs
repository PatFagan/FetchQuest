using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goap : MonoBehaviour
{
    public List<GoapAction> GoapActions; // list of relevant goap actions

    float timeBetweenActions = 2f;

    void Start()
    {
        // start goap cycle
        StartCoroutine(RunState());
    }

    IEnumerator RunState()
    {
        // run through goap actions
        // run first action with all preconditions met
        for (int i = GoapActions.Count - 1; i >= 0; i--)
        {
            // found action with preconditions met
            if (GoapActions[i].CheckPreconditions())
            {
                print("found goap action to run");
                GoapActions[i].RunAction();
                break;
            }
        }
        
        // delay between actions
        yield return new WaitForSeconds(timeBetweenActions);

        // wait until action is complete to continue
        //yield return new WaitUntil(() => GoapActions[i].pausedGoap == false);
        StartCoroutine(RunState());
    }
}