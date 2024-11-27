using UnityEngine;

public class RefillAmmo : MonoBehaviour
{
    [SerializeField] private int refillAmount = 5; // Value to set the bullet count to
    [SerializeField] private string playerTag = "Player"; // Tag to identify the player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            // Try to get the ShotgunJump component from the player
            ShotgunJump shotgunJump = other.GetComponent<ShotgunJump>();

            if (shotgunJump != null)
            {
                // Set the bullet count to the refill amount
                shotgunJump.bulletCount = refillAmount;
                Debug.Log("Ammo refilled to: " + refillAmount);
            }
            else
            {
                Debug.LogWarning("ShotgunJump script not found on the player.");
            }

            // Optionally destroy the refill object after use
            Destroy(gameObject);
        }
    }
}
