using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WheelchairController : MonoBehaviour {
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    public ParticleSystem moveParticles; // Reference to the Particle System

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the WheelchairController object.");
        }

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        moveParticles.Stop(); // Ensure particles are initially stopped
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical"); // Use GetAxisRaw for continuous movement

        // Calculate forward/backward movement
        Vector3 moveDirection = transform.forward * verticalInput * moveSpeed * Time.fixedDeltaTime;

        // Apply force to the Rigidbody instead of directly setting velocity
        rb.AddForce(moveDirection, ForceMode.VelocityChange);

        // Apply rotation based on horizontal input
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * horizontalInput * turnSpeed * Time.fixedDeltaTime);
        if (Mathf.Abs(horizontalInput) > 0.1f) // Check if there's input to rotate
        {
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        // Check if moving to play or stop the particle system
        if (Mathf.Abs(verticalInput) > 0.1f)
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