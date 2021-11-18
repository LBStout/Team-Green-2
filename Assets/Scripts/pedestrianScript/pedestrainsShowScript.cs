using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] people;
    public float defaultTimer = 10f;
    private float timer = 0f;

    // Start is called before the first frame update
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

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = defaultTimer;


        }

    }
}
