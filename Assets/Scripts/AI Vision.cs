using UnityEngine;

public class Vision : MonoBehaviour
{
    public AIStateMachine ai;
    public float visionRange = 10f;

    void Update()
    {
        if (ai != null && ai.player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, ai.player.position);

            if (distanceToPlayer < visionRange)
            {
                ai.currentState = AIStateMachine.AIState.chase;
            }
        }
        else
            Debug.LogWarning("AIStateMachine or Player reference is missing!", this);
    }
}