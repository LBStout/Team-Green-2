using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiFlipScript : MonoBehaviour
{
    [Range(0.1f, 90f)]
    public float maxAngle = 40f;

    // Update is called once per frame
    void Update()
    {
        Vector3 localRot = gameObject.transform.localEulerAngles;
        localRot.z = Mathf.Clamp(localRot.z, -maxAngle, maxAngle);
        gameObject.transform.localEulerAngles = localRot;
    }
}
