using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class VRCarController : MonoBehaviour
{
    public SteeringWheel wheel;

    public InputActionAsset actions;
    public InputActionReference triggerRight;
    public InputActionReference triggerRightClick;
    public InputActionReference triggerLeft;
    public InputActionReference triggerLeftClick;

    //input variables
    private float steerAngle;
    private bool isReverse;

    public float motorForce =  50f;
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

    private void HandleMotor() {

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

        //if brake is not pressed
        if (brake == 0)
        {

            if (isReverse)
            {
                accelerate *= -1;
            }

            frontLeftWheelCollider.motorTorque = accelerate * motorForce;
            frontRightWheelCollider.motorTorque = accelerate * motorForce;
        } 
           

        brakeForce = brake * 3000f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void HandleSteering() {
        steerAngle = -maxSteerAngle * (wheel.Angle / wheel.angleLimit);
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
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
