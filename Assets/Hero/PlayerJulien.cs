using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;


public class PlayerJulien : MonoBehaviour
{
    // Animation du perso 

    Animator PlayerAnimator;


    public float speed = 10f;
    public float gravity = 20f;
    public float jumpSpeed = 15f;


    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;



    public Vector3 moveD = Vector3.zero;
    CharacterController Cac;

    CapsuleCollider playerCollider;



    // Start is called before the first frame update
    void Start()
    {
        Cac = GetComponent<CharacterController>();
        PlayerAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Cac.isGrounded)
        {
            moveD = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveD = transform.TransformDirection(moveD);
            moveD *= jumpSpeed;

            if(Input.GetButton("Jump"))
            {
                moveD.y = jumpSpeed;
            }
        }
        moveD.y -= gravity * Time.deltaTime;
        transform.Rotate (Vector3.up *Input.GetAxis("Mouse X")*Time.deltaTime * jumpSpeed * 10);

        Cac.Move(moveD * Time.deltaTime);
    }
}