using UnityEngine;

public class IceMagicOrb : MonoBehaviour
{
    public float speed = 10f;  // Speed of the orb
    [SerializeField] private GameObject explosionPrefab; // The prefab for the explosion effect
    private float lifespan = 5f; // Time before the orb is destroyed
    private Rigidbody rb;  // To move the orb with physics

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Get the direction from the camera's forward vector
        Camera cam = Camera.main;
        Vector3 moveDirection = cam.transform.forward;

        // Apply force to move the orb in the camera's forward direction
        rb.linearVelocity = moveDirection * speed;

        // Destroy the orb after a certain lifespan
        Destroy(gameObject, lifespan);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the orb hits an object on the "WhatIsWall" or "WhatIsGround" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("whatIsWall") || 
            other.gameObject.layer == LayerMask.NameToLayer("whatIsGround"))
        {
            // Spawn the explosion effect at the orb's position
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);

            // Destroy the explosion after 1 second
            Destroy(explosion, 1f);

            // Destroy the orb
            Destroy(gameObject);
        }
    }
}
