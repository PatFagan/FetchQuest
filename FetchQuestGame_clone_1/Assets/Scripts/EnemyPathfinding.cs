using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    // variables
    NavMeshAgent navMeshAgent;
    bool wandering, scramming, hasScrammedOnce;
    float dist;
    public float wanderSpeed, scramSpeed, timeBetweenMoves;
    Vector3 wanderPos;
    Transform escapeLoc;

    Revolver revolverScript;
    // Start is called before the first frame update
    void Start()
    {
        // ref gun script to detect when gun is drawn
        revolverScript = GameObject.FindGameObjectWithTag("Revolver").GetComponent<Revolver>();
        
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>(); // get navAgent component
        wandering = true;

        hasScrammedOnce = false;

        escapeLoc = GameObject.Find("EscapeLoc").GetComponent<Transform>();

        // start wandering
        if (wandering)
        {
            navMeshAgent.speed = wanderSpeed;
            StartCoroutine(Wander());
        }
    }

    void FixedUpdate()
    {
        // calculate dist bw ai and targetLoc
        dist = Vector3.Distance(transform.position, wanderPos);

        scramming = revolverScript.drawn;

        // if gun is drawn, ai's begin scramming
        if (scramming && hasScrammedOnce == false)
        {
            StartCoroutine(Scram());
            hasScrammedOnce = true;
        }

        // enemy escapes at escape point
        if (wanderPos == escapeLoc.position && dist <= 2f)
        {
            Destroy(gameObject);
        }
    }

    // selects a random pos and pathfinds there
    IEnumerator Wander()
    {
        if (scramming == false)
        {
            wanderPos = new Vector3(Random.Range(-8f, 9f), gameObject.transform.position.y, Random.Range(-6f, 5f));

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
    }

    // increases wander speed
    IEnumerator Scram()
    {
        yield return new WaitForSeconds(.25f);
        timeBetweenMoves = .5f;
        navMeshAgent.speed = scramSpeed;

        yield return new WaitForSeconds(2f);
        wanderPos = escapeLoc.position; 
        navMeshAgent.SetDestination(wanderPos);
    }
}