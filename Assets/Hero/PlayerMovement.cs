using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    #region Camera Movement Variables
    public Camera playerCamera;
    public float mouseSensitivity = 3f;
    public float maxLookAngle = 90f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    #endregion

    #region Movement
    public float gravity = -20f;
    public float rotationSpeed = 90f;
    public float moveSpeed = 5f;
    public float maxVelocityChange = 7f;
    public float jumpSpeed = 10f;
    public bool isGrounded;
    Vector3 moveVelocity;
    Vector3 turnVelocity;
    #endregion
    CharacterController characterController;
    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Camera
        yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch = pitch - Input.GetAxis("Mouse Y") * mouseSensitivity;
        //Camera mouse direction
        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
        transform.localEulerAngles = new Vector3(0, yaw, 0);
        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        #endregion

        #region Jump
        if(characterController.isGrounded)
        {
            moveVelocity = transform.forward * moveSpeed * Input.GetAxis("Vertical");
            
            if(Input.GetButtonDown("Jump"))
            {
                moveVelocity.y = jumpSpeed;
                animator.SetBool("IsJumping", true);
            }
            if (!Input.GetButtonDown("Jump"))
            {
                animator.SetBool("IsJumping", false);
            }
        }
        moveVelocity.y += gravity * Time.deltaTime;
        characterController.Move(moveVelocity * Time.deltaTime);
        #endregion

        #region Animation
        if (Input.GetKey("z"))
        {
            animator.SetBool("IsWalking", true);
            moveSpeed = 5f;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("IsRunning", true);
                moveSpeed = 10f;
            }
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("IsRunning", false);
            }
        }
        if (!Input.GetKey("z"))
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
        }

        if (Input.GetKey("s"))
        {
            animator.SetBool("IsBackwarding", true);
            moveSpeed = 2f;
        }
        if (!Input.GetKey("s"))
        {
            animator.SetBool("IsBackwarding", false);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        

        Vector3 velocityChange = targetVelocity - rb.velocity;
        
        targetVelocity = transform.TransformDirection(targetVelocity) * moveSpeed;

        velocityChange.y = 0;
        rb.AddForce(velocityChange, ForceMode.Impulse);

    }

    
}
