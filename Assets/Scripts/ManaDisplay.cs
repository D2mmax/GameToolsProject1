using UnityEngine;
using TMPro;

public class ManaDisplay : MonoBehaviour
{
    public TextMeshProUGUI manaText; // Reference to the TextMesh Pro text
    private PlayerMagicSystem playerMagicSystem; // Reference to your player's magic system

    private void Start()
    {
        playerMagicSystem = FindObjectOfType<PlayerMagicSystem>(); // Find the PlayerMagicSystem instance
    }

    private void Update()
    {
        // Update the mana text based on the player's current mana
        if (playerMagicSystem != null)
        {
            manaText.text = $"Mana: {playerMagicSystem.currentMana:F0}"; // Display current mana
        }
    }
}
