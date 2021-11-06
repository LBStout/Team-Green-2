using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTrigger : MonoBehaviour
{
    public bool sameSeed = false;
    public bool leftAllowed = true;

    public GameObject[] waypoints = new GameObject[0];

    private void Start()
    {
        if (!sameSeed)
        {
            int seed = System.DateTime.Now.Ticks.GetHashCode();
            Random.InitState(seed);
            //Debug.Log("Seed: " + seed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AgentControl controller = other.gameObject.GetComponent<AgentControl>();
        if (controller != null)
        {
            int chosen = (int)Random.Range(0f, waypoints.Length - 0.00001f - (leftAllowed && waypoints.Length > 1f ? 0f : 1f));
            //Debug.Log("Trigger Hit: " + chosen);
            controller.waypoint = waypoints[chosen];
        }
    }
}
