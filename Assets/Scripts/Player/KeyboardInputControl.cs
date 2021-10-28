using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInputControl : MonoBehaviour
{
    public float speed;
    public float rotationSpeed = 1;

    private Rigidbody rb;

    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //// Update is called once per frame
    void OnMove(InputValue movementValue)
    {

        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        //rb.AddForce(movement * speed);

        movement.Normalize();

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    //private void Update()
    //{

    //    float horizontalInput = Input.GetAxis("Horizontal");
    //    float verticalInput = Input.GetAxis("Vertical");

    //    Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
    //    movementDirection.Normalize();

    //    transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

    //    if (movementDirection != Vector3.zero)
    //    {
    //        Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
    //        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    //    }
    //}
}
