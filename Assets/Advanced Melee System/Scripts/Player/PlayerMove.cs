using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
    public float speed;
    public float jumpHeight;

    private bool isGrounded;
    private float verticalSpeed = 0f;

    [SerializeField] private Animator anim;
    public Transform cam;

    private bool isDodging = false;
    private float dodgeTimer = 0f;
    public float dodgeDistance = 5f;
    public float dodgeDuration = 0.5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGrounded();

        if (!isDodging)
        {
            HandleMovement();
            HandleJump();
            HandleDodge();
        }
        else
        {
            Dodge();
        }
    }

    void CheckGrounded()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && verticalSpeed < 0)
        {
            verticalSpeed = 0f;
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        AnimateMovement(horizontal, vertical);

        Vector3 movement = CalculateMovement(horizontal, vertical);

        if (movement.magnitude != 0f)
        {
            RotateCharacter(movement);
        }

        ApplyGravity();
        MoveCharacter(movement);
    }

    void AnimateMovement(float horizontal, float vertical)
    {
        bool isWalking = (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f);
        anim.SetBool("isWalking", isWalking);
    }

    Vector3 CalculateMovement(float horizontal, float vertical)
    {
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        movement = cam.transform.TransformDirection(movement);
        movement.y = 0f;
        movement.Normalize();
        return movement * speed * Time.deltaTime;
    }

    void RotateCharacter(Vector3 movement)
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z));
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            verticalSpeed += Physics.gravity.y * Time.deltaTime;
        }
    }

    void MoveCharacter(Vector3 movement)
    {
        movement.y = verticalSpeed * Time.deltaTime;
        controller.Move(movement);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalSpeed = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }

    void HandleDodge()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isDodging = true;
            dodgeTimer = 0f;
            anim.SetTrigger("canDodge");
        }
    }

    void Dodge()
    {
        dodgeTimer += Time.deltaTime;
        if (dodgeTimer < dodgeDuration)
        {
            Vector3 dodgeDirection = transform.forward * dodgeDistance;
            dodgeDirection.y = 0f;

            controller.Move(dodgeDirection * Time.deltaTime);
        }
        else
        {
            isDodging = false;
        }
    }
}
