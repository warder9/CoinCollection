using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectibleManager.instance.CollectItem();  // Notify manager
            Destroy(gameObject);  // Remove collectible
        }
    }
}
