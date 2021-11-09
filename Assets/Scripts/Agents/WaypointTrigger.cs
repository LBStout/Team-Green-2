using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTrigger : MonoBehaviour
{
    public bool sameSeed = false;

    private readonly List<Collider> agents = new List<Collider>();

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

    private void LateUpdate()
    {
        for (int i = 0; i < agents.Count; i++)
            UpdateWaypoint(agents[i]);
    }

    private void UpdateWaypoint(Collider agent)
    {
        AgentControl controller = agent.gameObject.GetComponent<AgentControl>();
        if (controller != null && controller.waypoint != null && controller.waypoint == this.gameObject)
        {
            int chosen = (int)Random.Range(0f, waypoints.Length - 0.00001f);
            //Debug.Log("Trigger Hit: " + chosen);
            controller.waypoint = waypoints[chosen];
            agents.Remove(agent);
        }
        else if (controller == null)
            agents.Remove(agent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!agents.Contains(other))
            agents.Add(other);
    }
}
