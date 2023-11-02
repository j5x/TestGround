using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController Controller;
    public float Speed;
    public float JumpHeight;
    private bool isGrounded;
    private bool isJumping = false;
    private float verticalSpeed = 0f;
    [SerializeField] private Animator anim;
    public Transform Cam;

    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Controller.isGrounded;

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        if (isGrounded && verticalSpeed < 0)
        {
            verticalSpeed = 0f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            verticalSpeed = Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("isWalking", true);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("isWalking", false);
        }

        Vector3 Movement = new Vector3(Horizontal, 0, Vertical);
        Movement = Cam.transform.TransformDirection(Movement);
        Movement.y = 0f;
        Movement.Normalize();
        Movement *= Speed * Time.deltaTime;

        if (Movement.magnitude != 0f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(Movement.x, 0, Movement.z));
        }

        if (!isGrounded)
        {
            verticalSpeed += Physics.gravity.y * Time.deltaTime;
        }

        Movement.y = verticalSpeed * Time.deltaTime;
        Controller.Move(Movement);
    }
}
