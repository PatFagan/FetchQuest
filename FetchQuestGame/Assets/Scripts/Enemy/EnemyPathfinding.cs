using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    // variables
    NavMeshAgent navMeshAgent;
    bool wandering, attacking, hasScrammedOnce;
    float dist;
    public float wanderSpeed, attackSpeed, timeBetweenMoves;
    Vector3 wanderPos;
    Transform escapeLoc;

    Revolver revolverScript;
    // Start is called before the first frame update
    void Start()
    {
        // ref gun script to detect when gun is drawn
        //revolverScript = GameObject.FindGameObjectWithTag("Revolver").GetComponent<Revolver>();
        
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>(); // get navAgent component
        wandering = true;

        hasScrammedOnce = false;

        attackSpeed = attackSpeed + Random.Range(-.5f, .5f);

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

        //attacking = revolverScript.drawn;
        attacking = true;

        // if gun is drawn, ai's begin attacking
        if (attacking && hasScrammedOnce == false)
        {
            StartCoroutine(Attack());
            hasScrammedOnce = true;
        }
    }

    // selects a random pos and pathfinds there
    IEnumerator Wander()
    {
        if (attacking == false)
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
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(.25f);
        timeBetweenMoves = .5f;
        navMeshAgent.speed = attackSpeed;

        wanderPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        navMeshAgent.SetDestination(wanderPos);

        StartCoroutine(Attack());
    }
}