using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    bool wandering, scramming;
    float dist;
    public float wanderSpeed, scramSpeed, timeBetweenMoves;
    Vector3 wanderPos;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        wandering = true;

        if (wandering)
        {
            navMeshAgent.speed = wanderSpeed;
            StartCoroutine(Wander());
        }
    }

    void Update()
    {
        dist = Vector3.Distance(transform.position, wanderPos);
        print(dist);

        if (scramming)
        {
            navMeshAgent.speed = scramSpeed;
        }
    }

    IEnumerator Wander()
    {
        wanderPos = new Vector3(Random.Range(-10f, 10f), gameObject.transform.position.y, Random.Range(-10f, 10f));
        navMeshAgent.SetDestination(wanderPos);

        yield return new WaitUntil(() => dist <= 4f);
        print("cont");
        yield return new WaitForSeconds(timeBetweenMoves);
        StartCoroutine(Wander());
    }
}