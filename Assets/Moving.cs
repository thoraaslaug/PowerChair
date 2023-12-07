using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentThrust;

    // Settings
    [SerializeField] private float motorForce = 100f;
    [SerializeField] private float steeringAngle = 100f;
    [SerializeField] private ParticleSystem moveParticles; // Reference to the Particle System

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f); // Adjust the center of mass for better vehicle physics

        moveParticles.Stop(); // Ensure particles are initially stopped
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        CheckMovement();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void HandleMotor()
    {
        currentThrust = verticalInput * motorForce;
        rb.AddForce(transform.forward * currentThrust);
    }

    private void HandleSteering()
    {
        currentSteerAngle = steeringAngle * horizontalInput;
        transform.rotation *= Quaternion.Euler(0, currentSteerAngle * Time.fixedDeltaTime, 0);
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelTransform);
    }

    private void UpdateSingleWheel(Transform wheelTransform)
    {
        Vector3 pos = wheelTransform.position;
        Quaternion rot = wheelTransform.rotation;

        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void CheckMovement()
    {
        if (Mathf.Abs(verticalInput) > 0.01f || Mathf.Abs(horizontalInput) > 0.01f)
        {
            PlayParticles();
        }
        else
        {
            StopParticles();
        }
    }

    private void PlayParticles()
    {
        if (!moveParticles.isPlaying)
        {
            moveParticles.Play();
        }
    }

    private void StopParticles()
    {
        if (moveParticles.isPlaying)
        {
            moveParticles.Stop();
        }
    }
}