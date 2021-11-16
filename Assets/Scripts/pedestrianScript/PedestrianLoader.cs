using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianLoader : MonoBehaviour
{
    private GameObject player;
    private PedestrianController controller;
    private NavMeshAgent agent;
    private Renderer childRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = gameObject.GetComponent<PedestrianController>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        childRenderer = gameObject.GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance > 20f || !childRenderer.isVisible)
            {
                controller.enabled = false;
                agent.enabled = false;
            }
            else if (distance < 20f && childRenderer.isVisible)
            {
                controller.enabled = true;
                agent.enabled = true;
            }
        }
    }
}
