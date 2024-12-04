using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovementAdvanced pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private PlayerMovementAdvanced _playerMovement;
    private bool slideButtonHeld; // Tracks if slide key is held down
    private bool wasGrounded;     // Tracks if player was grounded in the previous frame

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovementAdvanced>();
        
        _playerMovement = GetComponent<PlayerMovementAdvanced>();

        startYScale = playerObj.localScale.y;
        wasGrounded = pm.grounded;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Track if the slide key is being held down
        slideButtonHeld = Input.GetKey(slideKey);

        // Grounded transition check: starts slide on landing if slide key is held
        if (pm.grounded && !wasGrounded && slideButtonHeld && (horizontalInput != 0 || verticalInput != 0))
        {
            StartSlide();
        }

        // Begin slide if slide key is pressed while grounded and moving
        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0) && pm.grounded)
        {
            StartSlide();
        }

        // Stop sliding if the slide key is released while sliding
        if (Input.GetKeyUp(slideKey) && pm.sliding)
        {
            StopSlide();
        }

        // Update grounded state for next frame
        wasGrounded = pm.grounded;
    }

    private void FixedUpdate()
    {
        // Continue sliding movement if currently sliding
        if (pm.sliding)
        {
            SlidingMovement();
        }
    }

    private void StartSlide()
    {
        if (pm.wallrunning || !pm.grounded) return;

        pm.sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Sliding on a flat or uphill surface
        if (!pm.OnSlope() || rb.linearVelocity.y > -0.1f)
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
            slideTimer -= Time.deltaTime;
        }
        // Sliding down a slope
        else
        {
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }

        if (slideTimer <= 0)
            StopSlide();
    }

    private void StopSlide()
    {
        pm.sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
}
