using UnityEngine;
using UnityEngine.AI;

public class AIStateMachine : MonoBehaviour
{

    public enum AIState {patrol, chase, search}
    public AIState currentState;

    public Transform player;
    public NavMeshAgent agent;
    public float visionrange = 10f;
    public float searchTime = 3f;

    private Vector3 lastknownposition;
    private bool playerSpotted;
    private float searchTimer;
    private float stuckTimer;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

    private void Update()
    {
        switch (currentState)
        {
            case AIState.patrol:
                Patrol();
                break;
            case AIState.chase:
                Chase();
                break;
            case AIState.search:
                Search();
                break;
        }
    }

    void Patrol()
    {
        if (agent.remainingDistance<0.5f)
        {
            Vector3 newpos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            agent.SetDestination(newpos);
        }
    }

    void Chase()
    {
        if(player != null)
        {
            lastknownposition = player.position;
            agent.SetDestination(player.position);
        }
    }

    void Search()
    {
        agent.SetDestination(lastknownposition);

        if(agent.remainingDistance<0.5f)
        {
            currentState = AIState.patrol;
        }
    }

    void CheckForPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance<visionrange)
        {
            currentState = AIState.chase;
        }

        else if (currentState == AIState.chase)
        {
            currentState = AIState.search;
        }
    }
}
