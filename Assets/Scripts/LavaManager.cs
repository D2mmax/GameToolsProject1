using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaManager : MonoBehaviour
{
    [SerializeField] private GameObject lava; // The GameObject that acts as the lava
    [SerializeField] private float scaleIncrease = 4f; // The amount to increase the Y scale by per second
    [SerializeField] private float maxHeight = 50f; // The maximum height the lava can reach
    private Vector3 initialScale; // The initial scale of the lava

    private bool isLavaRising = false; // Track if lava is rising

    private void Start()
    {
        // Store the initial scale of the lava
        if (lava != null)
        {
            initialScale = lava.transform.localScale;
        }
    }

    private void Update()
    {
        if (isLavaRising && lava != null)
        {
            // Increase the scale of the lava on the Y axis, up to the maxHeight
            Vector3 newScale = lava.transform.localScale;
            newScale.y += scaleIncrease * Time.deltaTime;

            // Clamp the Y scale to the maxHeight
            if (newScale.y > maxHeight)
            {
                newScale.y = maxHeight;
                isLavaRising = false; // Stop rising if maxHeight is reached
            }

            lava.transform.localScale = newScale;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player activated the lava rising trigger!");
            StartLavaRising();
        }
    }

    private void StartLavaRising()
    {
        isLavaRising = true; // Start the lava rising logic
    }

    public void ResetLava()
    {
        if (lava != null)
        {
            // Reset the lava to its initial scale
            lava.transform.localScale = initialScale;
            isLavaRising = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the lava trigger area!");
        }
    }
}
