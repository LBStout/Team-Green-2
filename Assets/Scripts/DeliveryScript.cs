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
        deliveryPoints = GameObject.FindGameObjectsWithTag("Delivery");
        randNum = Random.Range(0, deliveryPoints.Length);
        transform.position = deliveryPoints[randNum].transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            boxes.gameObject.SetActive(!boxes.gameObject.activeSelf);
            randNum = Random.Range(0, deliveryPoints.Length);
            transform.position = deliveryPoints[randNum].transform.position;
        }
    }

}
