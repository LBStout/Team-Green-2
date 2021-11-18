using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryScript : MonoBehaviour
{
    public CrossReference boxes;
    private GameObject[] deliveryPoints;
    private int randNum;

    // Start is called before the first frame update
    void Start()
    {
        //Get all possible delivery points, and randomly assign one to start
        deliveryPoints = GameObject.FindGameObjectsWithTag("Delivery");
        randNum = Random.Range(0, deliveryPoints.Length);
        transform.position = deliveryPoints[randNum].transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        //When the player collides with the delivery point...
        if (other.gameObject.tag == "player")
        {
            //Pick up/Drop off the package...
            boxes.gameObject.SetActive(!boxes.gameObject.activeSelf);
            //Then assign a random new delivery point
            randNum = Random.Range(0, deliveryPoints.Length);
            transform.position = deliveryPoints[randNum].transform.position;
        }
    }

}
