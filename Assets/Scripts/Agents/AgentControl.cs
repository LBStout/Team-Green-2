using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentControl : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject waypoint;

    private Vector3 targetPoint;

    // Update is called once per frame
    void Update()
    {
        if (waypoint != null)
        {
            targetPoint = waypoint.transform.position;
            agent.SetDestination(targetPoint);
        }

        if (Vector3.Distance(targetPoint, transform.position) < 2.0f)
        {
            //Debug.Log(waypoint.name);
        }
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
