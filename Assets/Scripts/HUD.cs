using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI normalAmmoText;
    public TextMeshProUGUI jumpPadAmmoText;
    public TextMeshProUGUI speedPadAmmoText;

    // Health Bar UI element
    public Slider healthBar;

    private int normalAmmo = 15;
    private int jumpPadAmmo = 10;
    private int speedPadAmmo = 10;


    public enum PlatformType { Normal, JumpPad, SpeedPad }
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
        else if (Input.GetKeyDown(KeyCode.R)){
            EquipPlatform(PlatformType.SpeedPad);
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
        else if (equippedPlatformType == PlatformType.SpeedPad && speedPadAmmo > 0){
            speedPadAmmo--;
        }

        UpdateAmmoHUD();
    }

    public void UpdateAmmoHUD()
    {
        normalAmmoText.text = "Normal Bullets: " + normalAmmo;
        jumpPadAmmoText.text = "Jump Bullets: " + jumpPadAmmo;
        speedPadAmmoText.text = "Speed Bullets: " + speedPadAmmo;

        if (equippedPlatformType == PlatformType.Normal)
        {
            normalAmmoText.fontStyle = FontStyles.Bold;
            jumpPadAmmoText.fontStyle = FontStyles.Normal;
            speedPadAmmoText.fontStyle = FontStyles.Normal;
        }
        else if (equippedPlatformType == PlatformType.JumpPad)
        {
            normalAmmoText.fontStyle = FontStyles.Normal;
            jumpPadAmmoText.fontStyle = FontStyles.Bold;
            speedPadAmmoText.fontStyle = FontStyles.Normal;
        }
        else if (equippedPlatformType == PlatformType.SpeedPad){
            normalAmmoText.fontStyle = FontStyles.Normal;
            jumpPadAmmoText.fontStyle = FontStyles.Normal;
            speedPadAmmoText.fontStyle = FontStyles.Bold;

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
