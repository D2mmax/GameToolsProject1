using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float bounceForce = 10f; // The force to apply when bouncing

   void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Debug.Log("Player entered the trigger!");

        // Optional: Add specific behavior for the player
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 bounceDirection = transform.up;
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
}
