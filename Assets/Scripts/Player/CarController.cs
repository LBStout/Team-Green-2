using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CarController : MonoBehaviour
{
    public InputActionAsset actions;
    public InputActionReference movement;

    //const strings
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    //input variables
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;

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


    //void OnMove(InputValue movementValue)
    //{

    //    Vector2 movementVector = movementValue.Get<Vector2>();

    //    horizontalInput = movementVector.x;
    //    verticalInput = movementVector.y;
    //    isBreaking = Keyboard.current.spaceKey.wasPressedThisFrame;
    //    if (Keyboard.current.spaceKey.wasPressedThisFrame) {
    //        Debug.Log("A white space is pressed");
    //    }

    //}

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        //To adjust to VR controller input later
        isBreaking = Input.GetKey(KeyCode.Space);

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Hello");
        }


    }

    private void HandleMotor() {

        horizontalInput = movement.action.ReadValue<Vector2>().x;
        verticalInput = movement.action.ReadValue<Vector2>().y;
        
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        brakeForce = isBreaking ? 3000f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void HandleSteering() {
        steerAngle = maxSteerAngle * horizontalInput;
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
