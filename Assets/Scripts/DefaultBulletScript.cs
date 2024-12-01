using UnityEngine;

public class DefaultBulletScript : MonoBehaviour
{
    public GameObject spawnPlatform;
    public string targetTag;
    public float bounceMultiplier = 1.0f;

    private Rigidbody rb;
    private SimpleBulletPool bulletPool; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletPool = Object.FindAnyObjectByType<SimpleBulletPool>(); 
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Vector3 impactPoint = other.ClosestPointOnBounds(transform.position);
            Vector3 normal = Vector3.up;

            if (other is BoxCollider boxCollider)
            {
                normal = boxCollider.transform.up;
            }

            SpawnPlatform(impactPoint, normal, spawnPlatform);
            bulletPool.ReturnBullet(gameObject);
        }
        else if (other.CompareTag("NormalPlatform"))
        {
            Vector3 bounceDirection = Vector3.Reflect(rb.linearVelocity, other.transform.up);
            rb.linearVelocity = bounceDirection * bounceMultiplier;
        }
    }

    void SpawnPlatform(Vector3 position, Vector3 normal, GameObject platformType)
    {
        GameObject platform = Instantiate(platformType, position, Quaternion.identity);
        platform.transform.up = normal;
    }
}
