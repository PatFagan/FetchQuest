using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    // variables
    NavMeshAgent navMeshAgent;
    bool wandering, scramming;
    float dist;
    public float wanderSpeed, scramSpeed, timeBetweenMoves;
    Vector3 wanderPos;

    Revolver revolverScript;
    // Start is called before the first frame update
    void Start()
    {
        // ref gun script to detect when gun is drawn
        revolverScript = GameObject.FindGameObjectWithTag("Revolver").GetComponent<Revolver>();
        
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>(); // get navAgent component
        wandering = true;

        // start wandering
        if (wandering)
        {
            navMeshAgent.speed = wanderSpeed;
            StartCoroutine(Wander());
        }
    }

    void Update()
    {
        // calculate dist bw ai and targetLoc
        dist = Vector3.Distance(transform.position, wanderPos);

        scramming = revolverScript.drawn;

        // if gun is drawn, ai's begin scramming
        if (scramming)
        {
            StartCoroutine(Scram());
        }
    }

    // selects a random pos and pathfinds there
    IEnumerator Wander()
    {
        wanderPos = new Vector3(Random.Range(-10f, 10f), gameObject.transform.position.y, Random.Range(-10f, 10f));

        navMeshAgent.SetDestination(wanderPos);

        // wait until loc reached or time passed
        float pauseTime = 10f;
        yield return new WaitUntil(() =>
        {
            pauseTime -= Time.deltaTime;
            return dist <= 1f || pauseTime <= 0;
        });

        yield return new WaitForSeconds(timeBetweenMoves);
        StartCoroutine(Wander());
    }

    // increases wander speed
    IEnumerator Scram()
    {
        yield return new WaitForSeconds(.25f);
        timeBetweenMoves = 0f;
        navMeshAgent.speed = scramSpeed;
    }
}