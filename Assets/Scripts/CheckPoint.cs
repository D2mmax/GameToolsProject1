using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition; // Shared across instances

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your player GameObject is tagged correctly
        {
            lastCheckpointPosition = transform.position;
            Debug.Log("Checkpoint updated: " + lastCheckpointPosition);
        }
    }
}
