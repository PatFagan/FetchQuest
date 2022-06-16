using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public Transform camera;
    Transform orientation;

    //Other
    private Rigidbody rigidbody;

    //Rotation and look
    private float xRotation;
    public float sensitivity = 50f;
    private float sensMultiplier = 1f;

    //Movement
    float targetSpeed;
    public float moveSpeed = 4500;
    public bool grounded;
    public LayerMask whatIsGround;
    Vector3 movement;

    public float counterMovement = 0.175f;
    public float maxSlopeAngle = 35f;

    //Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;
    public float slideForce = 400;
    public float slideCounterMovement = 0.2f;

    //Jumping
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    public float jumpForce;

    //Input
    float horizontal, vertical;
    bool jumping, sprinting, crouching;
    public float sprintSpeed = 40.0f;
    public float sprintDuration = .75f;
    private float sprintTimer = 0.0f;
    public float sprintCooldown = 1f;

    //Sliding
    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        orientation = gameObject.GetComponent<Transform>();
    }

    void Start()
    {
        playerScale = transform.localScale;
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
    }

    /// <summary>
    /// Find user input. Should put this in its own class but im lazy
    /// </summary>
    private void MyInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButton("Jump");
        crouching = Input.GetKey(KeyCode.LeftControl);

        // sprint timeout
        sprintTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Sprint") && sprintTimer < 0f)
        {
            StartCoroutine(Sprint());
        }

        //Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();
    }

    private void StartCrouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rigidbody.velocity.magnitude > 0.5f)
        {
            if (grounded)
            {
                rigidbody.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    private void StopCrouch()
    {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    IEnumerator Sprint()
    {
        sprinting = true;
        yield return new WaitForSeconds(sprintDuration);
        sprintTimer = sprintCooldown;
        sprinting = false;
    }

    private void Movement()
    {
        // extra gravity
        rigidbody.AddForce(Vector3.down * 500f);

        //If holding jump && ready to jump, then jump
        if (readyToJump && jumping) Jump();

        //If sliding down a ramp, add force down so player stays grounded and also builds speed
        if (crouching && grounded && readyToJump)
        {
            rigidbody.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }

        // set target speed based on move speed, sprint speed and if sprint is pressed
        targetSpeed = sprinting ? sprintSpeed : moveSpeed;
        
        // apply forces to move player
        rigidbody.AddForce(orientation.transform.forward * vertical * targetSpeed);
        rigidbody.AddForce(orientation.transform.right * horizontal * targetSpeed);
    }

    private void Jump()
    {
        if (grounded && readyToJump)
        {
            // add jump force
            rigidbody.AddForce(Vector2.up * jumpForce);

            // if jumping while falling, reset y velocity.
            Vector3 vel = rigidbody.velocity;
            if (rigidbody.velocity.y < 0.5f)
                rigidbody.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rigidbody.velocity.y > 0)
                rigidbody.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCooldown);
            readyToJump = false;
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = camera.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        camera.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    // check if grounded
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}