using UnityEngine;
using TMPro;
using UnityEngine.UI; // Import UI namespace
using System.Collections;
public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager instance;

    public int totalCollectibles = 6;
    private int collectedCount = 0;
    public TextMeshProUGUI collectibleText; // TextMeshPro UI
    public Image gameEndScreen; // UI Image instead of GameObject

    void Awake()
    {
        instance = this;

        // Ensure the game end screen is hidden at start
        if (gameEndScreen != null)
        {
            gameEndScreen.color = new Color(1, 1, 1, 0); // Fully transparent
        }

        UpdateUI();
    }

    public void CollectItem()
    {
        collectedCount++;
        UpdateUI();

        if (collectedCount >= totalCollectibles)
        {
            EndGame();
        }
    }

    void UpdateUI()
    {
        collectibleText.text = $"Collected {collectedCount} / {totalCollectibles}";
    }

    void EndGame()
    {
        if (gameEndScreen != null)
        {
            StartCoroutine(FadeInGameEndScreen());
        }

        Time.timeScale = 0f; // Stop game
    }

    IEnumerator FadeInGameEndScreen()
    {
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            gameEndScreen.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }
}
