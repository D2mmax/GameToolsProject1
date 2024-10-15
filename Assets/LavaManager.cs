using UnityEngine;

public class LavaManager : MonoBehaviour
{
    [SerializeField] private GameObject lava; // The GameObject that acts as the lava
    private float scaleIncrease = 4f; // The amount to increase the Y scale by

    private void Start()
    {
        // Start the repeating scale increase
        InvokeRepeating(nameof(IncreaseLavaScale), 1f, 1f);
    }

    private void IncreaseLavaScale()
    {
        if (lava != null)
        {
            // Increase the scale of the lava on the Y axis
            Vector3 newScale = lava.transform.localScale;
            newScale.y += scaleIncrease;
            lava.transform.localScale = newScale;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Functionality to be added later
    }
}
