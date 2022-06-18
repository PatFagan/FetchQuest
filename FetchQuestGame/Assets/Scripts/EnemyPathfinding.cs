using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    bool wandering, scramming;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        wandering = true;

        if (wandering)
        {
            StartCoroutine(Wander());
        }
    }

    IEnumerator Wander()
    {
        Vector3 wanderPos = new Vector3(Random.Range(-10f, 10f), gameObject.transform.position.y, Random.Range(-10f, 10f));
        navMeshAgent.SetDestination(wanderPos);
        yield return new WaitUntil(() => gameObject.transform.position == wanderPos);
        print("cont");
        yield return new WaitForSeconds(3f);
        StartCoroutine(Wander());
    }
}