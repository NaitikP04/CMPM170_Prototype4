using UnityEngine;
using static PlayerController;

public class ShootingController : MonoBehaviour
{
    public SimpleBulletPool bulletPool; // Reference to the bullet pool
    public float projectileSpeed = 30f;
    public Camera mainCamera;

    public GameObject normalPlatformPrefab;
    public GameObject jumpPadPlatformPrefab;
    public GameObject chosenPlatform;
    public PlatformType platformName;

    public PlayerController playerController;

    public int normalPlatformAmmo = 9;
    public int jumpPadAmmo = 4;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            platformName = playerController.equippedPlatformType;

            if (HasAmmo(platformName))
            {
                Shoot();
                DeductAmmo(platformName);
            }
            else
            {
                Debug.Log("Out of ammo for " + platformName);
            }
        }
    }

    void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 shootDirection = (hit.point - transform.position).normalized;
            shootDirection.z = 0;

            platformName = playerController.equippedPlatformType;
            ChoosePlatform(platformName);

            // Get a bullet from the pool instead of instantiating
            GameObject projectile = bulletPool.GetBullet();
            projectile.transform.position = transform.position;
            projectile.transform.rotation = Quaternion.identity;

            DefaultBulletScript projectileScript = projectile.GetComponent<DefaultBulletScript>();
            projectileScript.spawnPlatform = chosenPlatform;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.linearVelocity = shootDirection * projectileSpeed;
        }
    }

    void ChoosePlatform(PlatformType Name)
    {
        switch (Name)
        {
            case PlayerController.PlatformType.jumpPad:
                chosenPlatform = jumpPadPlatformPrefab;
                break;
            default:
                chosenPlatform = normalPlatformPrefab;
                break;
        }
    }

    bool HasAmmo(PlatformType platformType)
    {
        switch (platformType)
        {
            case PlatformType.jumpPad:
                return jumpPadAmmo > 0;
            case PlatformType.Normal:
                return normalPlatformAmmo > 0;
            default:
                return false;
        }
    }

    void DeductAmmo(PlatformType platformType)
    {
        switch (platformType)
        {
            case PlatformType.jumpPad:
                jumpPadAmmo--;
                break;
            case PlatformType.Normal:
                normalPlatformAmmo--;
                break;
        }
    }
}
