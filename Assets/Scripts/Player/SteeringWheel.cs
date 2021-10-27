using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SteeringWheel : MonoBehaviour
{
    // The object holding the wheel.
    private GameObject m_Target;
    public GameObject Target
    {
        get
        {
            return m_Target;
        }
        set
        {
            lastAngle = Mathf.Infinity;
            m_Target = value;
        }
    }

    [Tooltip("Track the target and rotate the wheel.")]
    public bool trackTarget;

    // The current vector used for rotating the wheel.
    private Vector3 projected;

    private Vector3 m_WorldUp;
    private Vector3 WorldUp
    { 
        get
        {
            if (m_WorldUp == Vector3.zero)
            {
                // Grab current neutral position upward
                Vector3 upwardPoint = Vector3.up + transform.position;
                Vector3 a = transform.position - upwardPoint;
                Vector3 n = -transform.up;

                float projAngle = Vector3.Angle(a.normalized, n);
                float length2Plane = Mathf.Cos(projAngle * Mathf.Deg2Rad) * a.magnitude;
                Vector3 projectedPoint = upwardPoint + n * length2Plane;

                m_WorldUp = Vector3.Normalize(projectedPoint - transform.position);
            }
            return m_WorldUp;
        }
        set
        {
            m_WorldUp = value;
        }
    }

    // The current angle of the wheel.
    public float Angle { get; private set; }
    private float lastAngle;

    [Tooltip("How much the wheel can be turned in either direction."), Range(0f, 180f)]
    public float limitThreshold = 150f;

    [Tooltip("How fast the wheel should return to neutral when the target isn't being tracked.\n[0: The wheel doesn't return to neutral]\n[1: The wheel snaps back to neutral immediately]"), 
        Range(0f, 1f)]
    public float returnSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Angle = 0f;
        lastAngle = Mathf.Infinity;
        m_WorldUp = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Queue new up to be calculated.
        WorldUp = Vector3.zero;

        if (trackTarget && Target != null)
        {
            // Project hand point onto 2D plane of steering wheel
            Vector3 a = transform.position - Target.transform.position;
            Vector3 n = -transform.up;
            float projAngle = Vector3.Angle(a.normalized, n);
            float length2Plane = Mathf.Cos(projAngle * Mathf.Deg2Rad) * a.magnitude;
            Vector3 projectedPoint = Target.transform.position + n * length2Plane;
            projected = Vector3.Normalize(projectedPoint - transform.position);

            // Get the angle of the vector
            float currentAngle = Vector3.SignedAngle(WorldUp, projected, -transform.up);

            // Store initial angle if previously not tracked
            if (lastAngle == Mathf.Infinity)
                lastAngle = currentAngle;

            // If stored is different from current, calculate the difference to apply
            float difference = currentAngle - lastAngle;
            bool positive = difference > 0f;

            // Validate whether or not the new angle is a valid rotation
            float newAngle = Angle - difference;
            bool validRotation = 
                Mathf.Abs(difference) < 180f                    // If absolute value of the difference is greater than 180, then the angle has swapped signs and should be ignored this frame.
                && (Mathf.Abs(newAngle) < limitThreshold        // If the angle after applying the difference is within threshold range, then it is valid.
                || Mathf.Abs(Angle) < limitThreshold            // If the current angle is within the threshold range, then it is valid to be clamped at the threshold.
                || Angle > 0f && positive                       // If the current angle is past the positive threshold, then it is only valid if the correction is negative.
                || Angle < 0f && !positive);                    // If the current angle is past the negative threshold, then it is only valid if the correction is positive.

            if (validRotation) 
            {
                // Clamp newAngle to threshold so either threshold is a reachable value
                newAngle = Mathf.Clamp(newAngle, -limitThreshold, limitThreshold);
                float appliedDifference = Angle - newAngle;

                // Rotate the wheel by the clamped difference
                transform.RotateAround(transform.position, -transform.up, appliedDifference);
                Angle = Vector3.SignedAngle(-transform.forward, WorldUp, -transform.up);
            }

            // Update the last known angle
            lastAngle = currentAngle;
        }
        else
        {
            // Mark last angle as unset by setting it as an impossible value
            lastAngle = Mathf.Infinity;

            // Set projected to WorldUp so it is shown by Gizmos
            projected = WorldUp;

            // Gradually move wheel back to neutral position
            float remainingOffset = Vector3.SignedAngle(-transform.forward, WorldUp, -transform.up);
            float blend = (remainingOffset != 0) ? returnSpeed * remainingOffset * Time.deltaTime: 0f;
            transform.RotateAround(transform.position, -transform.up, blend);
            Angle = Vector3.SignedAngle(-transform.forward, WorldUp, -transform.up);
        }
    }

    private void OnDrawGizmos()
    {
        // Only show Gizmos in Play Mode
        if (!Application.isPlaying)
            return;

        // Target vector
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position + (projected * 0.35f), 0.1f);

        // Forward vector
        Gizmos.color = Gizmos.color = new Color(0, 1, 0, 0.5f);
        Vector3 upPosition = transform.position + (-transform.forward * 0.35f);
        Gizmos.DrawSphere(upPosition, 0.05f);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SteeringWheel)), CanEditMultipleObjects]
public class SteeringWheelEditor : Editor
{
    private SteeringWheel instance;

    private readonly static GUIContent targetPrefix = new GUIContent("Target", "The Game Object currently holding the wheel.");

    private void OnEnable()
    {
        instance = (SteeringWheel)target;
    }

    public override void OnInspectorGUI()
    {
        // Make sure object is obtained first
        if (instance == null)
            return;
        
        // Update serialized values
        serializedObject.Update();

        // Serialized properties
        EditorGUILayout.PropertyField(serializedObject.FindProperty("limitThreshold"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("returnSpeed"));

        // Properties only usable in Play Mode
        if (!Application.isPlaying)
            EditorGUILayout.HelpBox("The following properties should only be set during runtime.", MessageType.Info);
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.BeginDisabledGroup(!Application.isPlaying);
        instance.Target = (GameObject)EditorGUILayout.ObjectField(targetPrefix, instance.Target, typeof(GameObject), true); // Don't need to set dirty bit if set in Play Mode only.
        EditorGUILayout.PropertyField(serializedObject.FindProperty("trackTarget"));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();

        // Apply changed serialized properties
        serializedObject.ApplyModifiedProperties();
    }
}
#endif