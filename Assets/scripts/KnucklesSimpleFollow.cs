using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class KnucklesScreamerFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float screamDistance = 1.5f;
    public float minSoundInterval = 0.3f;
    public float maxSoundInterval = 2f;
    public AudioSource proximitySound;
    public AudioClip screamerClip;

    public Image screamerImage;
    public float screamerDuration = 1f;
    public float screenShakeDuration = 0.5f;
    public float screenShakeIntensity = 0.2f;

    private float nextSoundTime = 0f;
    private bool screamerTriggered = false;
    public IEnumerator StunEnemy(float duration)
    {
        float originalSpeed = speed;  // Store original speed
        speed = 0;                    // Stop movement
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;         // Restore speed after stun
    }
    void Start()
    {
        if (screamerImage != null)
        {
            screamerImage.color = new Color(1, 1, 1, 0); // Ensure it's fully hidden at start
        }
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            transform.LookAt(player);

            // Play proximity sound effect more frequently the closer the model is
            if (Time.time >= nextSoundTime)
            {
                if (proximitySound != null && !proximitySound.isPlaying)
                {
                    proximitySound.Play();
                }

                float interval = Mathf.Lerp(minSoundInterval, maxSoundInterval, distance / 10f);
                nextSoundTime = Time.time + interval;
            }

            // Trigger screamer when very close
            if (distance <= screamDistance && !screamerTriggered)
            {
                TriggerScreamer();
            }
        }
    }

    void TriggerScreamer()
    {
        screamerTriggered = true;
        if (proximitySound != null)
        {
            proximitySound.Stop();
        }

        if (screamerClip != null)
        {
            AudioSource.PlayClipAtPoint(screamerClip, transform.position);
        }

        // Show Screamer Image
        if (screamerImage != null)
        {
            StartCoroutine(ShowScreamerImage());
        }

        // Start screen shake effect
        StartCoroutine(ScreenShakeEffect());

        // Delay Game Over screen to show after screamer disappears
        StartCoroutine(ShowGameOverAfterScreamer());
    }

    IEnumerator ShowScreamerImage()
    {
        screamerImage.color = new Color(1, 1, 1, 1); // Fully visible
        yield return new WaitForSeconds(screamerDuration);
        screamerImage.color = new Color(1, 1, 1, 0); // Hide it after duration
    }

    IEnumerator ShowGameOverAfterScreamer()
    {
        yield return new WaitForSeconds(screamerDuration); // Wait for the screamer effect to finish

        // Show Game Over screen now
        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOverScreen();
        }
    }

    IEnumerator ScreenShakeEffect()
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = Camera.main.transform.position;

        while (elapsedTime < screenShakeDuration)
        {
            float xOffset = Random.Range(-screenShakeIntensity, screenShakeIntensity);
            float yOffset = Random.Range(-screenShakeIntensity, screenShakeIntensity);

            Camera.main.transform.position = originalPosition + new Vector3(xOffset, yOffset, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = originalPosition; // Reset camera position
    }

    
}
