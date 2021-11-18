using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class VRCarController : MonoBehaviour
{
    public SteeringWheel wheel;

    public AudioSource runningSound;
    public AudioSource brakingSound;

    public InputActionAsset actions;
    public InputActionReference triggerRight;
    public InputActionReference triggerRightClick;
    public InputActionReference triggerLeft;
    public InputActionReference triggerLeftClick;

    //input variables
    private float steerAngle;
    private bool isReverse;

    private bool isRunning;
    private bool isBraking;

    public float maxPitch = 4f;
    public float minPitch = 0.5f;

    public float maxVelocity = 16.76f;
    public float motorForce = 500f;
    public float brakeForce = 0f;
    public float maxSteerAngle = 30f;

    //get reference to wheel collider
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheeelTransform;
    public Transform frontRightWheeelTransform;
    public Transform rearLeftWheeelTransform;
    public Transform rearRightWheeelTransform;

    private void Start()
    {
        actions.Enable();
    }

    private void FixedUpdate()
    {

        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    public void OnReverse()
    {
        isReverse = !isReverse;
    }

    private void HandleMotor()
    {
        //get value from input
        float brake = 0f;
        if (triggerLeftClick.action.ReadValue<float>() == 1f)
            brake = 1f;
        else
            brake = triggerLeft.action.ReadValue<float>();
        float accelerate = 0f;
        if (triggerRightClick.action.ReadValue<float>() == 1f)
            accelerate = 1f;
        else
            accelerate = triggerRight.action.ReadValue<float>();

        float speed = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        //change properties based on the input
        //if brake is not pressed
        if (brake == 0)
        {
            if (isReverse)
            {
                accelerate *= -1;
            }
            if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > maxVelocity)
                accelerate = 0;

            frontLeftWheelCollider.motorTorque = accelerate * motorForce;
            frontRightWheelCollider.motorTorque = accelerate * motorForce;

            float blend = Mathf.Abs(speed / (maxVelocity * 0.8f));
            Debug.Log(speed);
            runningSound.pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Clamp(blend, 0, 1));

            //if (speed < minPitch) {
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
            //if this is the first time to run the car
            if (!isRunning)
            {
                isRunning = true;
                runningSound.Play();
            }
            isBraking = false;

        }
        else {
            //stop running and accelarating
            isRunning = false;
            //if this is the first time call braking
            if (!isBraking && speed >= 8)
            {
                brakingSound.time = 1f;
                brakingSound.Play();
                runningSound.pitch = minPitch;
                isBraking = true;
            }

            else if (!isBraking && speed < 2f * (3.5f / 5))
            {
                isBraking = true;
            }

            else if (!isBraking && speed < 8)
            {
                brakingSound.time = 1.3f;
                brakingSound.Play();
                runningSound.pitch = minPitch;
                isBraking = true;
            }

            if (speed < 1.67625f * (3.5f/5))
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
           

        brakeForce = brake * 4000f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void HandleSteering() {
        steerAngle = -maxSteerAngle * (wheel.Angle / wheel.angleLimit);
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.angularDrag = 0.5f + Mathf.Lerp(0, 1.5f, rb.velocity.magnitude / maxVelocity);
    }


    private void UpdateWheels()
    {

        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheeelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheeelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheeelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheeelTransform);

    }


    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans) {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }


}
