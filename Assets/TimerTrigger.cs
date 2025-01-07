using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    [SerializeField] private TimerAndLeaderboard timerManager; // Reference to the TimerAndLeaderboard script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player is triggering the collider
        {
            if (this.CompareTag("StartTrigger")) // Check if this is the start trigger
            {
                timerManager.StartTimer(); // Call StartTimer() on the TimerAndLeaderboard script
            }
            else if (this.CompareTag("EndTrigger")) // Check if this is the end trigger
            {
                timerManager.StopTimer(); // Call StopTimer() on the TimerAndLeaderboard script
            }
        }
    }
}
