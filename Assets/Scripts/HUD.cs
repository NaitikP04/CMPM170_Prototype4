using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI normalAmmoText;
    public TextMeshProUGUI jumpPadAmmoText;

    // Health Bar UI element
    public Slider healthBar;

    private int normalAmmo = 9;
    private int jumpPadAmmo = 4;

    public enum PlatformType { Normal, JumpPad }
    private PlatformType equippedPlatformType;

    void Start()
    {
        UpdateAmmoHUD(); // Initialize ammo count
        SetMaxHealth(100); // Initialize health bar to maximum health
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EquipPlatform(PlatformType.Normal);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            EquipPlatform(PlatformType.JumpPad);
        }

        if (Input.GetMouseButtonDown(0))
        {
            UseAmmo();
        }
    }

    void EquipPlatform(PlatformType platformType)
    {
        equippedPlatformType = platformType;
        UpdateAmmoHUD();
    }

    void UseAmmo()
    {
        if (equippedPlatformType == PlatformType.Normal && normalAmmo > 0)
        {
            normalAmmo--;
        }
        else if (equippedPlatformType == PlatformType.JumpPad && jumpPadAmmo > 0)
        {
            jumpPadAmmo--;
        }

        UpdateAmmoHUD();
    }

    public void UpdateAmmoHUD()
    {
        normalAmmoText.text = "Normal Bullets: " + normalAmmo;
        jumpPadAmmoText.text = "Jump Bullets: " + jumpPadAmmo;

        if (equippedPlatformType == PlatformType.Normal)
        {
            normalAmmoText.fontStyle = FontStyles.Bold;
            jumpPadAmmoText.fontStyle = FontStyles.Normal;
        }
        else if (equippedPlatformType == PlatformType.JumpPad)
        {
            normalAmmoText.fontStyle = FontStyles.Normal;
            jumpPadAmmoText.fontStyle = FontStyles.Bold;
        }
    }

    // Health Bar Methods
    public void SetMaxHealth(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        healthBar.value = health;
    }
}
