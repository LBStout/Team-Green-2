using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryScript : MonoBehaviour
{
    public AudioSource boxPickup;
    public AudioSource boxDropoff;
    public GameObject boxes;
    private GameObject[] deliveryPoints;
    private int randNum;
    private bool noiseToggle = false;

    // Start is called before the first frame update
    void Start()
    {
        //Get all possible delivery points, and randomly assign one to start
        deliveryPoints = GameObject.FindGameObjectsWithTag("Delivery");
        boxes = GameObject.FindGameObjectWithTag("boxes");
        randNum = Random.Range(0, deliveryPoints.Length);
        transform.position = deliveryPoints[randNum].transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        //When the player collides with the delivery point...
        if (other.gameObject.tag == "player")
        {

            if (noiseToggle == false)
            {
                boxPickup.Play();
                noiseToggle = true;
            }
            else
            {
                boxDropoff.Play();
                noiseToggle = false;
            }

            //Pick up/Drop off the package...
            if (boxes.gameObject != null)
                boxes.gameObject.SetActive(!boxes.gameObject.activeSelf);

            //Then assign a random new delivery point
            randNum = Random.Range(0, deliveryPoints.Length);
            transform.position = deliveryPoints[randNum].transform.position;
        }
    }

}
