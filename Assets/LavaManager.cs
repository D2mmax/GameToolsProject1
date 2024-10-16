using UnityEngine;
using UnityEngine.SceneManagement;
public class LavaManager : MonoBehaviour
{
    [SerializeField] private GameObject lava; // The GameObject that acts as the lava
    [SerializeField] private float scaleIncrease = 4f; // The amount to increase the Y scale by
    public Transform spawnPoint; // Reference to the spawn point

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
        if (other.CompareTag("Player"))
        {
            Debug.Log("player detected");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
