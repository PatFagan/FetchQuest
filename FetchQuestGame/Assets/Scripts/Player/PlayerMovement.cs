using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public bool controller;

    Transform camera;
    Transform orientation;
    private Rigidbody rigidbody;
    public Quaternion cameraRotation;

    public GameObject revolver;
    public GameObject cameraPrefab;

    // Rotation and look
    private float xRotation;
    public float sensitivity = 50f;

    // Movement
    public float moveSpeed = 4500;
    float horizontal, vertical;
    Vector3 movement;
    float distToGround;
    bool sprintCharged = true;

    public float jumpForce, sprintForce;

    [HideInInspector]
    public bool dead;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        orientation = gameObject.GetComponent<Transform>();
        
        distToGround = GetComponent<Collider>().bounds.extents.y;
        
        // create player camera
        cameraPrefab.GetComponent<CameraMovement>().parentPlayer = gameObject.transform;

        Instantiate(cameraPrefab); 
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();

        // spawn gun
        revolver.GetComponent<Revolver>().parentPlayer = gameObject; // set parent player
        Instantiate(revolver);

        ResetCursor();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        if (!dead)
        {
            MyInput();
            Look();
            Jump();
            Sprint();
        }
        if (dead)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            rigidbody.velocity = Vector3.zero;
        }
    }

    public void ResetCursor()
    {
        // camera cursor settings
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

        // apply forces to move player
        rigidbody.AddForce(orientation.transform.forward * vertical * moveSpeed);
        rigidbody.AddForce(orientation.transform.right * horizontal * moveSpeed);
    }

    private void Jump()
    {
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.75f);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody.AddForce(orientation.transform.up * moveSpeed * jumpForce);
        }
    }

    private void Sprint()
    {
        if (Input.GetButtonDown("Sprint") && sprintCharged)
        {
            rigidbody.AddForce(orientation.transform.forward * vertical * sprintForce); 
            rigidbody.AddForce(orientation.transform.right * horizontal * sprintForce); 
            
            print(orientation.transform.forward * vertical * sprintForce); 
            print(orientation.transform.right * horizontal * sprintForce); 

            StartCoroutine(SprintCooldown());
        }
    }

    IEnumerator SprintCooldown()
    {
        sprintCharged = false;
        yield return new WaitForSeconds(1.5f);
        sprintCharged = true;
    }

    // some camera direction nonsense
    private float desiredX;
    private void Look()
    {
        float mouseX, mouseY;

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (controller)
        { 
            mouseX = Input.GetAxis("LookXController");
            mouseY = Input.GetAxis("LookYController");
        }

        mouseX *= sensitivity * Time.fixedDeltaTime;
        mouseY *= sensitivity * Time.fixedDeltaTime;

        // Find current look rotation
        Vector3 rot = camera.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        // Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Perform the rotations
        camera.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
        
        cameraRotation = camera.transform.localRotation;
    }
}