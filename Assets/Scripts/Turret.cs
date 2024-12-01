

using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform player;
    public Transform turretHead;         
    public float rotationSpeed = 20f;
    public float detectionRange = 15f;
    public float detectionAngle = 45f;
    public float rotationExtension = 15f; // Extra rotation beyond detection angle
    public GameObject bulletPrefab;
    public Transform firePoint;          
    public float fireRate = 1f;

    private LineRenderer lineRenderer;
    public int circleSegments = 50; 

    private float nextFireTime;
    private Quaternion initialRotation; 

    void Start()
    {
        initialRotation = turretHead.localRotation;

        lineRenderer = GetComponent<LineRenderer>();
        DrawRangeIndicator();
    }

    void Update()
    {
        if (IsPlayerInSight())
        {
            TrackPlayer();

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            RotateTurret();
        }
    }

    void DrawRangeIndicator()
    {
        lineRenderer.positionCount = circleSegments + 1;
        float angle = 0f;

        for (int i = 0; i <= circleSegments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * detectionRange;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * detectionRange;
            lineRenderer.SetPosition(i, new Vector3(x, 0, z) + transform.position);
            angle += 360f / circleSegments;
        }
    }

    void RotateTurret()
    {
        // Extend the rotation beyond the detection angle
        float extendedAngle = detectionAngle + rotationExtension;
        float rotationY = Mathf.PingPong(Time.time * rotationSpeed, extendedAngle * 2) - extendedAngle;

        turretHead.localRotation = initialRotation * Quaternion.Euler(0, rotationY, 0);
    }


    void TrackPlayer()
    {
        Vector3 directionToPlayer = (player.position - turretHead.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        turretHead.rotation = Quaternion.Slerp(turretHead.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    bool IsPlayerInSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > detectionRange)
            return false;

        float angleToPlayer = Vector3.Angle(turretHead.forward, directionToPlayer.normalized);
        return angleToPlayer <= detectionAngle;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        TurretBullet bulletScript = bullet.GetComponent<TurretBullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = 10; 
        }
    }

    void OnDrawGizmos() //debugging
    {
        if (turretHead == null) return;

        Gizmos.color = new Color(1, 0, 0, 0.3f); 

        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector3 forward = turretHead.forward;
        Vector3 leftBoundary = Quaternion.Euler(0, -detectionAngle, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, detectionAngle, 0) * forward;

        Gizmos.DrawRay(transform.position, leftBoundary * detectionRange);
        Gizmos.DrawRay(transform.position, rightBoundary * detectionRange);

        int numSegments = 20; 
        for (int i = 0; i <= numSegments; i++)
        {
            float angle = -detectionAngle + (2f * detectionAngle / numSegments) * i;
            Vector3 segmentDirection = Quaternion.Euler(0, angle, 0) * forward;
            Vector3 segmentEnd = transform.position + segmentDirection * detectionRange;
            Gizmos.DrawLine(transform.position, segmentEnd);
        }
    }
}
