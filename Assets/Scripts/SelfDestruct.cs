using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifetime = 5f;  // Time in seconds before the object is destroyed

    void Start()
    {
        // Destroy the game object after `lifetime` seconds
        Destroy(gameObject, lifetime);
    }
}
