using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carsSoundEffectScript : MonoBehaviour
{

    public AudioSource runningSound;

    public float maxPitch = 4f;
    public float minPitch = 0.5f;

    void Start()
    {
        runningSound.Play();
    }


    // Update is called once per frame
    void Update()
    {


        float speed = gameObject.GetComponent<Rigidbody>().velocity.magnitude;

        float blend = Mathf.Abs(speed / (5f * 0.8f));
        runningSound.pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Clamp(blend, 0, 1));

        //if (speed < minPitch)
        //{
        //    runningSound.pitch = minPitch;
        //}
        //else if (speed > maxPitch)
        //{
        //    runningSound.pitch = maxPitch;
        //}
        //else
        //{
        //    runningSound.pitch = speed;
        //}


    }
    }

