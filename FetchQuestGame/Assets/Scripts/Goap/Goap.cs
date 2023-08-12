using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goap : MonoBehaviour
{
    public List<GoapAction> GoapActions;
    public List<GoapAction> initalGoapActions;

    public GameObject goapActionKeeper;
    public bool runningGoap; // for debugging to easily disable goap

    bool gnomeRunning = false;
    
    public float timeBetweenActions;
    public bool pausedGoap; // resume bool when each action is complete

    MeleeAttack meleeScript;
    RangedAttack rangedScript;
    void setListOfGoapActions()
    {
        int index = 0;

        meleeScript = goapActionKeeper.GetComponent<MeleeAttack>();
        GoapActions[index] = meleeScript;
        initalGoapActions[index] = meleeScript;
        index++;

        rangedScript = goapActionKeeper.GetComponent<RangedAttack>();
        GoapActions[index] = rangedScript;
        initalGoapActions[index] = rangedScript;
        index++;

    }

    void Start()
    {
        // get all goap actions
        setListOfGoapActions();

        // start goap cycle
        //if(PhotonNetwork.isMasterClient)
            if (runningGoap)
                StartCoroutine(RunState());
    }

    public void NextState()
    {
        StartCoroutine(RunState());
    }

    IEnumerator RunState()
    {
        yield return new WaitForSeconds(timeBetweenActions);

        //if (!PhotonNetwork.isMasterClient)
            yield return null;

        //gnomeSort(GoapActions);

        // run through goap actions
        // run first action with all preconditions met
        for (int i = GoapActions.Count - 1; i >= 0; i--)
        {
            if (GoapActions[i].CheckPreconditions())
            {
                //loops through the inital order of the actions
                for (int j = 0; j < initalGoapActions.Count; ++j)
                {
                    //Checks what action should be run with inital order to send to all clients
                    if (initalGoapActions[j] == GoapActions[i])
                    {
                        RunAction(j);//
                        break;
                    }
                }
                //GoapActions[i].RunAction();
                //yield return new WaitUntil(() => gnomeRunning == false);
                //GoapActions[i].cost += 1;
                //GoapActions.RemoveAt(i);
                break;
            }
        }

        yield return new WaitUntil(() => pausedGoap == false);
        StartCoroutine(RunState());
    }

    //[PunRPC]
    float RunAction(int i)
    {
        initalGoapActions[i].RunAction();
        //isActing = true;
        //Make sure the same action is run across all clients
        return 0;
        
    }

    void gnomeSort(List<GoapAction> list)
    {
        gnomeRunning = true;

        int i = 0;

        while (i < list.Count)
        {
            if (i == 0)
                i++;
            if (list[i - 1].cost >= list[i].cost)
                i++;
            else
            {
                swapVar(list[i], list[i - 1]);
                i--;
            }
        }

        gnomeRunning = false;
    }

    // swaps two values
    void swapVar(GoapAction a, GoapAction b)
    {
        GoapAction temp;
        temp = a;
        a = b;
        b = temp;
    }

}