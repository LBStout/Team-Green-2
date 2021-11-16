using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentControl : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject waypoint;

    private Vector3 targetPoint;
    private bool shouldStop; 
    private Vector3 stopVelocity = new Vector3 (0.0f, 0.0f, 0.0f);

    // Update is called once per frame
    void Update()
    {
        if ((waypoint != null) && (!shouldStop))
        {
            targetPoint = waypoint.transform.position;
            agent.SetDestination(targetPoint);
        }
        else if (shouldStop)
        {
            agent.velocity = stopVelocity;
            agent.SetDestination(agent.transform.position);
        }

        if (Vector3.Distance(targetPoint, transform.position) < 2.0f)
        {
            //Debug.Log(waypoint.name);
        }
    }

    private void OnTriggerEnter(Collider other) {
        shouldStop = true;
    }

    void OnDrawGizmos()
    {
        if (targetPoint != Vector3.zero)
        {
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Gizmos.DrawSphere(targetPoint, 1);
        }
        else if (!Application.isPlaying && waypoint != null)
        {
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Gizmos.DrawSphere(waypoint.transform.position, 1);
        }
    }
}
