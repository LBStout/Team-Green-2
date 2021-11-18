using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private AgentControl agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = transform.parent.gameObject.GetComponent<AgentControl>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<WaypointTrigger>() == null)
        {
            agent.shouldStop = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.GetComponent<WaypointTrigger>() == null)
        {
            agent.shouldStop = false;
        }
    }
}
