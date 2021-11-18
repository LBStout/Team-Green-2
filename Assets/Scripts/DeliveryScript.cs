using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryScript : MonoBehaviour
{

    private GameObject[] deliveryPoints;

    // Start is called before the first frame update
    void Start()
    {
        deliveryPoints = GameObject.FindGameObjectsWithTag("Delivery");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
