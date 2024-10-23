using System.Collections;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.UI;
using UnityEditor.SearchService;
using UnityEngine;

public class ShotgunJump : MonoBehaviour
{
    public Transform camHolder;      // Reference to the camera holder to determine the direction
    public Rigidbody playerRb;       // Reference to the player's Rigidbody

    [SerializeField] private KeyCode shootButton = KeyCode.Mouse0;  // Button used to shoot (default: Left Mouse Button)
    [SerializeField] private float jumpStrength = 10f;              // Strength of the shotgun jump
    [SerializeField] private GameObject projectile;

    [SerializeField] private float _cooldDown = 3f;

    private bool _canShoot = true;

    private void Update()
    {
        if (Input.GetKeyDown(shootButton) && _canShoot)
        {
            StartCoroutine(RocketJump());
        }
    }

    private IEnumerator RocketJump()
    {   
        _canShoot = false;
        // Calculate the opposite direction to where the camera is looking
        Vector3 jumpDirection = -camHolder.forward;

        if (playerRb.velocity.y < 0)
        {
            playerRb.AddForce(jumpDirection * (jumpStrength + -playerRb.velocity.y), ForceMode.Impulse);
        }
        else
        {
            playerRb.AddForce(jumpDirection * jumpStrength, ForceMode.Impulse);
        }
        
        yield return new WaitForSeconds(_cooldDown);

        _canShoot = true;
    }
}
