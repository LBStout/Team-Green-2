using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedestrainsShowScript : MonoBehaviour
{

    GameObject[] people;

    // Start is called before the first frame update
    void Start()
    {
        people = GameObject.FindGameObjectsWithTag("pedestrian");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
