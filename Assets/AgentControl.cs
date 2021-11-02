using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentControl : MonoBehaviour
{
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        print("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 targetPoint = new Vector3(-300.9f, 0.0f, 127.0f);
        Vector3 targetPoint = new Vector3(-360.9f, 0.0f, 140.0f);
        agent.SetDestination(targetPoint);
    }
}
