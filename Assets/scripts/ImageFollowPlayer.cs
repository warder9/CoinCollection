using UnityEngine;

public class ImageFollowPlayer : MonoBehaviour
{
    public Transform player; // Assign your player in the Inspector
    public float followSpeed = 5f;
    public float heightOffset = 2f; // Adjust if the object is too low/high

    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + new Vector3(0, heightOffset, 0);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
