using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerMove : MonoBehaviour
{
 
 
    CharacterController Controller;
 
    public float Speed;

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
        float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("isWalking", true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("isWalking", false);
        }
 
        Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
        Movement.y = 0f;
 
 
 
        Controller.Move(Movement);
 
        if (Movement.magnitude != 0f)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Cam.GetComponent<CameraMove>().sensivity * Time.deltaTime);
 
 
            Quaternion CamRotation = Cam.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;
 
            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);
 
        }
    }
 
}