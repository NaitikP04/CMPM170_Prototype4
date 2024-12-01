using UnityEngine;  
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    // Reference to the respawn canvas UI
    public GameObject respawnCanvas;
    public GameObject creditsPanel;
    public void Start()
    {
        Time.timeScale = 1;
    }
    // This method loads a scene based on the scene name
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Call this method to show the respawn UI
    public void ShowRespawnUI()
    {
        if (respawnCanvas != null)
        {
            respawnCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // Trigger detection for colliding with the respawn trigger object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowRespawnUI();
            
        }
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditsPanel.SetActive(false);
    }
}
