using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public Image gameOverImage;  // Reference the Image component
    public float restartDelay = 3f;  // Time before restarting

    void Start()
    {
        gameOverImage.color = new Color(1, 1, 1, 0); // Hide Game Over image at start (fully transparent)
    }

    public void ShowGameOverScreen()
    {
        gameOverImage.color = new Color(1, 1, 1, 1); // Make Game Over image fully visible
        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay); // Wait before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart level
    }
}
