using UnityEngine;
using System.Collections;

public class GunPickupAndShoot : MonoBehaviour
{
    public Transform gunHolder; // Assign where the gun should be held (e.g., Player's hand)
    public float stunDuration = 3f;
    public AudioClip shootSound;
    public ParticleSystem muzzleFlash;

    private bool hasGun = false;
    private GameObject gunInstance;

    void Update()
    {
        // Check if player is near the gun and presses "E" to pick up
        if (!hasGun && Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Gun"))
                {
                    PickUpGun(col.gameObject);
                    break;
                }
            }
        }

        // Shoot when pressing left mouse button and have gun
        if (hasGun && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void PickUpGun(GameObject gun)
    {
        gunInstance = gun;
        gunInstance.transform.SetParent(gunHolder);
        gunInstance.transform.localPosition = Vector3.zero;
        gunInstance.transform.localRotation = Quaternion.identity;
        hasGun = true;
    }

    void Shoot()
    {
        if (shootSound != null) AudioSource.PlayClipAtPoint(shootSound, transform.position);
        if (muzzleFlash != null) muzzleFlash.Play();

        // Find enemy and apply stun
        KnucklesScreamerFollow enemy = FindObjectOfType<KnucklesScreamerFollow>();
        if (enemy != null)
        {
            StartCoroutine(enemy.StunEnemy(stunDuration));
        }

        // Remove gun after shooting
        Destroy(gunInstance);
        hasGun = false;
    }
}
