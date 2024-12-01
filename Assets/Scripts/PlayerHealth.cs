using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    public GameObject deathUI;
    public HUD hud;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);
        hud.SetHealth(currentHealth); 


        if (currentHealth <= 0)
        {
            Die();
            if (deathUI != null)
            {
                deathUI.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    void Die()
    {
        Debug.Log("Player Died");
    }
}



