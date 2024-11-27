using System.Collections;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform player; // Assign your player GameObject here
    public Vector3 startPosition; // Initial spawn position
    public float respawnDelay = 2f; // Delay before respawning
    public int startingBulletCount = 5; // Desired bullet count value upon respawn

    private ShotgunJump shotgunJump; // Reference to the ShotgunJump script

    private void Start()
    {
        // Ensure a start position if no checkpoint has been triggered yet
        if (Checkpoint.lastCheckpointPosition == Vector3.zero)
        {
            Checkpoint.lastCheckpointPosition = startPosition;
        }

        // Get the ShotgunJump component (ammo management)
        shotgunJump = player.GetComponent<ShotgunJump>(); // Replace with your actual component if it's named differently
    }

    public void RespawnPlayer()
    {
        // Start the respawn coroutine
        StartCoroutine(RespawnCoroutine());

        // Reset all LavaManager instances
        LavaManager[] lavaManagers = FindObjectsOfType<LavaManager>();
        
        foreach (var lavaManager in lavaManagers)
        {
            lavaManager.ResetLava(); // Reset lava
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        // Optional: Disable player controls or show death animation/sound
        yield return new WaitForSeconds(respawnDelay);

        // Move the player to the last checkpoint position
        if (player != null)
        {
            player.position = Checkpoint.lastCheckpointPosition;

            // Directly reset the player's ammo (bulletCount) to the starting value
            if (shotgunJump != null)
            {
                shotgunJump.bulletCount = startingBulletCount; // Directly set the bullet count
                
            }
            else
            {
                Debug.LogError("ShotgunJump script is not attached to the player!");
            }
        }
        else
        {
            Debug.LogError("Player Transform is not assigned to the Respawn Manager!");
        }

        // Optional: Re-enable player controls
    }
}
