using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
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

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        orientation = gameObject.GetComponent<Transform>();
        
        distToGround = GetComponent<Collider>().bounds.extents.y;
        
        // create player camera
        cameraPrefab.GetComponent<StickToPlayer>().parentPlayer = gameObject.transform;
        cameraPrefab.name = "Camera" + GameObject.FindGameObjectsWithTag("Player").Length;
        Instantiate(cameraPrefab); 
        camera = GameObject.Find(cameraPrefab.name+"(Clone)").GetComponent<Transform>();

        // spawn gun
        revolver.GetComponent<Revolver>().parentPlayer = gameObject; // set parent player
        Instantiate(revolver);

        // camera cursor settings
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
        Jump();
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
        //rigidbody.velocity = new Vector3(orientation.transform.forward.x * movement.x * moveSpeed, 0f, orientation.transform.right.x * movement.z * moveSpeed);
        rigidbody.AddForce(orientation.transform.forward * vertical * moveSpeed);
        rigidbody.AddForce(orientation.transform.right * horizontal * moveSpeed);
    }

    private void Jump()
    {
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.75f);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody.AddForce(orientation.transform.up * moveSpeed * 30f);
        }
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
        
        cameraRotation = camera.transform.localRotation;
    }
}