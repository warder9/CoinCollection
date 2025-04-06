using System.Collections;
using UnityEngine;
using TMPro;

public class InstructionText : MonoBehaviour
{
    public TextMeshProUGUI instructionText; // Assign in the Inspector
    public string message = "Press 'E' to interact";
    public float displayDuration = 6f;

    void Start()
    {
        instructionText.text = message;
        instructionText.gameObject.SetActive(true);
        StartCoroutine(HideTextAfterDelay());
    }

    IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        instructionText.gameObject.SetActive(false);
    }
}
