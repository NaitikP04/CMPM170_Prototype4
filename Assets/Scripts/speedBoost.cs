using UnityEngine;

public class speedBoost : MonoBehaviour
{
    public float speedBonus = 400f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.moveSpeed = playerController.OGmoveSpeed+ speedBonus;
            }
        }
    }
}
