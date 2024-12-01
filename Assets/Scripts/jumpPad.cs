using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpBoost = 10f;
    public float boostMultiplier = 1.5f; 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.rigidbody;
            if (playerRb != null)
            {
                // Determine the boost direction based on player position relative to the jump pad
                Vector3 directionToPlayer = (collision.transform.position - transform.position).normalized;
                
                // Check if the player is above or below the jump pad
                Vector3 boostDirection = Vector3.zero;
                if (Vector3.Dot(directionToPlayer, transform.up) > 0)
                {
                    boostDirection = transform.up;
                }
                else
                {
                    boostDirection = -transform.up;
                }

                float scaledBoost = playerRb.linearVelocity.magnitude * boostMultiplier;
                Vector3 finalBoost = boostDirection * Mathf.Max(scaledBoost, jumpBoost);

                playerRb.linearVelocity = Vector3.zero;
                playerRb.AddForce(finalBoost, ForceMode.Impulse);
            }
        }
    }
}
