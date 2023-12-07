using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform target;
    public float force;

    private Rigidbody rb;
    
    [SerializeField] private GameObject startPosition;
    private GameObject respawnPosition;
    public AudioSource Source;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the Ball object.");
        }
        respawnPosition = startPosition;

    }
    public void Respawn(){
       // Source.Play();

        gameObject.transform.position = respawnPosition.transform.position;
    }
    void Shoot()
    {
        if (target != null && rb != null)
        {
            Vector3 shootDirection = (target.position - transform.position).normalized;
            rb.AddForce(shootDirection * force, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Target or Rigidbody not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger. Shooting...");
            Shoot();
        }
        else
        {
            Debug.Log("Something else triggered the collider.");
        }
    }
}