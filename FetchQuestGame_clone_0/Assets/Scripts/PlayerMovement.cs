using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public Transform camera;
    Transform orientation;
    private Rigidbody rigidbody;

    // Rotation and look
    private float xRotation;
    public float sensitivity = 50f;

    // Movement
    public float moveSpeed = 4500;
    float horizontal, vertical;
    Vector3 movement;

    //Jumping
    private float distToGround;
    bool isGrounded;
    private float jumpTimer;
    public float jumpDuration;
    public float jumpForce;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        orientation = gameObject.GetComponent<Transform>();

        distToGround = GetComponent<Collider>().bounds.extents.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        MyInput();
        Look();

        // check if grounded
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);

        // jumping
        if (isGrounded == true) { jumpTimer = jumpDuration; }

        // remove jump potential after jump button is lifted
        if (Input.GetButtonUp("Jump")) { jumpTimer = 0f; }

        if (Input.GetButton("Jump")) // jump
        {
            if (jumpTimer > 0f)
            {
                print("ground");
                rigidbody.AddForce(Vector3.up * jumpForce * Time.deltaTime);
                rigidbody.velocity += new Vector3(0f, jumpForce, 0f);
                jumpTimer -= Time.deltaTime;
            }
        }

        // increase fall speed
        if (rigidbody.velocity.y < -0.1)
        {
            rigidbody.velocity += Vector3.up * Physics2D.gravity.y * (jumpForce / 1500f) * Time.deltaTime;
        }
    }

    // set axis input values
    private void MyInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector3(horizontal, 0f, vertical);
    }

    private void Movement()
    {
        // extra gravity
        rigidbody.AddForce(Vector3.down * 500f);

        // movement
        rigidbody.AddForce(orientation.transform.forward * vertical * moveSpeed);
        rigidbody.AddForce(orientation.transform.right * horizontal * moveSpeed);
    }

    // some camera direction nonsense
    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;

        // Find current look rotation
        Vector3 rot = camera.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        // Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Perform the rotations
        camera.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }
}