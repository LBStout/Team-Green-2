using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiFlipScript : MonoBehaviour
{
    [Range(0.1f, 180f)]
    public float maxAngle = 89f;
    public float defaultTimer = 3f;

    private float timer = -1f;

    // Update is called once per frame
    void Update()
    {
        // Get the angle of the vector
        float currentAngle = Vector3.SignedAngle(transform.up, Vector3.up, transform.forward);
        //Debug.Log(currentAngle);
        
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                transform.Translate(new Vector3(0f, 0.5f, 0f), Space.World);
                Vector3 localRot = transform.localEulerAngles;
                localRot.z = 0f;
                transform.localEulerAngles = localRot;
            }
        }

        if (timer < 0f && Mathf.Abs(currentAngle) > maxAngle)
            timer = defaultTimer;
        else if (Mathf.Abs(currentAngle) < maxAngle)
            timer = -1f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
