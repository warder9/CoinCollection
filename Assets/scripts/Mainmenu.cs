using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This function loads the game scene when the Play button is clicked
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Change "GameScene" to your actual game scene name
    }

    // This function quits the game when the Quit button is clicked
    public void QuitGame()
    {
        
        Application.Quit(); // Quits the game (only works in a built application)
    }
}
