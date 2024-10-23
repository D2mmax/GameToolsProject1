using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public Transform camHolder;   // Reference to the camera holder for vertical rotation
    public Transform orientation; // Reference to the orientation for horizontal rotation

    private void Update()
    {
        // Apply the camera's vertical rotation and orientation's horizontal rotation
        transform.rotation = Quaternion.Euler(camHolder.eulerAngles.x, orientation.eulerAngles.y, 0);
    }
}
