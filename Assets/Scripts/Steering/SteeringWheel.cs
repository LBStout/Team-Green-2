using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    public GameObject target;
    public bool trackTarget;

    private float storedDifference;
    private Vector3 projected;

    public float Angle { get; private set; }
    public float limitThreshold = 150f;

    [Range(0f, 1f)]
    public float returnSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Angle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Grab current neutral position upward
        Vector3 originalRotation;
        {
            Vector3 upwardPoint = Vector3.up + transform.position;
            Vector3 a = transform.position - upwardPoint;
            Vector3 n = -transform.up;
            float projAngle = Vector3.Angle(a.normalized, n);
            float length2Plane = Mathf.Cos(projAngle * Mathf.Deg2Rad) * a.magnitude;
            Vector3 projectedPoint = upwardPoint + n * length2Plane;
            originalRotation = Vector3.Normalize(projectedPoint - transform.position);
        }

        if (trackTarget && target != null)
        {
            // Project hand point onto 2D plane of steering wheel
            Vector3 a = transform.position - target.transform.position;
            Vector3 n = -transform.up;
            float projAngle = Vector3.Angle(a.normalized, n);
            float length2Plane = Mathf.Cos(projAngle * Mathf.Deg2Rad) * a.magnitude;
            Vector3 projectedPoint = target.transform.position + n * length2Plane;
            projected = Vector3.Normalize(projectedPoint - transform.position);

            // Get the angle difference between the two vectors
            float angleDifference = Vector3.SignedAngle(-transform.forward, projected, -transform.up);

            // Store initial difference if previously not tracked
            if (storedDifference == 420f)
                storedDifference = angleDifference;

            // If stored is different from current, calculate the correction needed to restore it
            float correction = storedDifference - angleDifference;
            bool positive = correction > 0f;
            float posCorrection = positive ? correction : -correction;

            // Validate whether or not the correction is a valid rotation
            bool validRotation = false;
            {
                if (Mathf.Abs(Angle) - posCorrection < limitThreshold)
                    validRotation = true;
                else if (Angle > 0f && 
                        ((angleDifference > Angle && angleDifference < 0f && angleDifference < storedDifference) || // Left half shrink
                         (angleDifference < Angle && angleDifference > 0f && angleDifference > storedDifference)))  // Left half grow
                    validRotation = true;
                else if (Angle < 0f &&
                        ((angleDifference < Angle && angleDifference < 0f && angleDifference > storedDifference) || // Right half shrink
                         (angleDifference > Angle && angleDifference > 0f && angleDifference < storedDifference)))  // Right half grow
                    validRotation = true;
                else
                    storedDifference = angleDifference;
            }

            // If valid, apply the correction to the wheel's rotation
            if (validRotation) 
            {
                transform.RotateAround(transform.position, -transform.up, -correction);
                float currentAngle = Vector3.SignedAngle(-transform.forward, originalRotation, -transform.up);
                Angle = currentAngle;
            }
            //Debug.Log(Angle + " | " + currentAngle + " | " + limitThreshold + " | " + (Angle - currentAngle));
        }
        else
        {
            // Mark difference as unset by setting it as an impossible value
            storedDifference = 420f;

            // Set projected to originalRotation so it is shown by Gizmos
            projected = originalRotation;

            // Gradually move wheel back to neutral position
            float remainingOffset = Vector3.SignedAngle(-transform.forward, originalRotation, -transform.up);
            float blend = (remainingOffset != 0) ? returnSpeed * remainingOffset * Time.deltaTime: 0f;
            transform.RotateAround(transform.position, -transform.up, blend);
        }
    }

    private void OnDrawGizmos()
    {
        // Target vector
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position + (projected * 0.35f), 0.1f);

        // Forward vector
        Gizmos.color = Gizmos.color = new Color(0, 1, 0, 0.5f);
        Vector3 upPosition = transform.position + (-transform.forward * 0.35f);
        Gizmos.DrawSphere(upPosition, 0.05f);
    }
}