using UnityEngine;

public class GPTcamera : MonoBehaviour
{
    public float sensitivity = 2f; // Adjust the sensitivity of mouse movement
    public Transform player; // Assign your player GameObject here in the inspector

    float mouseX, mouseY;
    float rotationX = 0f;

    void Update()
    {
        // Get mouse input for camera control
        mouseX = Input.GetAxis("Mouse X") * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Calculate camera rotation based on mouse input
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limit vertical rotation

        // Apply rotation to the camera and the player (optional)
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}