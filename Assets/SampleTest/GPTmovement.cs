using UnityEngine;

public class GPTmovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this speed as needed
    public Transform cameraTransform; // Assign your camera's Transform here in the inspector

    Rigidbody rb;
    Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Input handling for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction based on camera orientation
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        movement = (cameraForward * vertical + cameraRight * horizontal).normalized * moveSpeed;

        // Apply movement in the FixedUpdate method
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

        // Rotate the player towards the movement direction (optional)
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}