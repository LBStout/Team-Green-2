using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class pedestrainsShowScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] people;

    void Start()
    {
        people = GameObject.FindGameObjectsWithTag("pedestrian");

        for (int i = 0; i < people.Length; i++)
        {

            people[i].GetComponent<NavMeshAgent>().enabled = false;
            people[i].GetComponent<PedestrianController>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
