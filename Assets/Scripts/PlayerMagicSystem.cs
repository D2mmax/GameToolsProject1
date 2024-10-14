using System.Collections;
using UnityEngine;

public class PlayerMagicSystem : MonoBehaviour
{
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float manaRechargeRate = 2f;
    [SerializeField] private Transform castPoint; // The point where magic spawns

    [Header("Ice Orb Spell")]
    [SerializeField] private GameObject iceOrbPrefab; // The prefab of the ice orb
    [SerializeField] private float iceOrbManaCost = 20f;
    [SerializeField] private float iceOrbCooldown = 0.5f;

    [Header("Ice Rain Spell")]
    [SerializeField] private GameObject iceRainPrefab; // The prefab of the ice rain spell
    [SerializeField] private float iceRainManaCost = 50f;
    [SerializeField] private float iceRainCooldown = 2f;
    [SerializeField] private float spellRange = 50f;   // Range for raycast targeting
    [SerializeField] private LayerMask groundLayer;    // Layers that the spell can target (e.g., ground)

    [Header("Wall Spell")]
    [SerializeField] private GameObject wallPrefab;    // The prefab for the wall spell
    [SerializeField] private float wallManaCost = 30f;
    [SerializeField] private float wallCooldown = 3.5f;
    [SerializeField] private float wallRange = 10f;    // Range for wall placement

    public float currentMana;
    private bool isCooldown = false; // To check if cooldown is active

    void Start()
    {
        // Initialize mana to maximum at the start of the game
        currentMana = maxMana;
    }

    void Update()
    {
        // Regenerate mana over time
        if (currentMana < maxMana)
        {
            currentMana += manaRechargeRate * Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0f, maxMana); // Ensure mana doesn't go over max
        }

        // Check for magic casting
        if (Input.GetMouseButtonDown(0) && !isCooldown) // Left mouse button to cast Ice Orb
        {
            TryCastIceOrb();
        }
        else if (Input.GetMouseButtonDown(1) && !isCooldown) // Right mouse button to cast Ice Rain
        {
            TryCastIceRain();
        }
        else if (Input.GetKeyDown(KeyCode.E) && !isCooldown) // 'E' key to summon wall
        {
            TryCastWall();
        }
    }

    // Function to cast the ice orb spell
    private void TryCastIceOrb()
    {
        if (currentMana >= iceOrbManaCost)
        {
            // Deduct mana
            currentMana -= iceOrbManaCost;

            // Cast the ice orb
            CastMagic(iceOrbPrefab);

            // Start cooldown for the ice orb
            StartCoroutine(SpellCooldown(iceOrbCooldown));
        }
        else
        {
            Debug.Log("Not enough mana to cast the ice orb.");
        }
    }

    // Function to cast the ice rain spell
    private void TryCastIceRain()
    {
        if (currentMana >= iceRainManaCost)
        {
            // Raycast to find where the player is aiming
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, spellRange, groundLayer))
            {
                // Deduct mana
                currentMana -= iceRainManaCost;

                // Spawn the ice rain spell at the target location
                Vector3 targetPosition = hit.point;
                Instantiate(iceRainPrefab, targetPosition, Quaternion.identity);
                Debug.Log("Ice rain cast at: " + targetPosition);

                // Start cooldown for the ice rain spell
                StartCoroutine(SpellCooldown(iceRainCooldown));
            }
            else
            {
                Debug.Log("No valid target to cast the ice rain spell.");
            }
        }
        else
        {
            Debug.Log("Not enough mana to cast the ice rain.");
        }
    }

private void TryCastWall()
{
    if (currentMana >= wallManaCost)
    {
        // Deduct mana
        currentMana -= wallManaCost;

        // Use Raycast to determine where to place the wall
        RaycastHit hit;
        // Cast a ray from the camera's position in the direction the camera is facing
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10f))
        {
            // Calculate spawn position, adding an offset in the forward direction
            Vector3 spawnPosition = hit.point + hit.normal * 0.5f; // Adjust offset as needed

            // Create a rotation based on the camera's Y rotation, keeping X and Z fixed
            Quaternion wallRotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f);

            // Instantiate the wall at the calculated position and rotation
            GameObject spawnedWall = Instantiate(wallPrefab, spawnPosition, wallRotation);

            Debug.Log("Wall summoned at: " + spawnPosition);
        }
        else
        {
            Debug.Log("No valid surface found to place the wall.");
        }

        // Start cooldown for the wall spell
        StartCoroutine(SpellCooldown(wallCooldown));
    }
    else
    {
        Debug.Log("Not enough mana to summon the wall.");
    }
}




























    // General method to cast magic spells
    private void CastMagic(GameObject magicPrefab)
    {
        // Instantiate the spell prefab at the cast point
        Instantiate(magicPrefab, castPoint.position, castPoint.rotation);
        Debug.Log("Magic cast!");
    }

    // Coroutine for handling spell cooldowns
    private IEnumerator SpellCooldown(float cooldownDuration)
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
    }
}
