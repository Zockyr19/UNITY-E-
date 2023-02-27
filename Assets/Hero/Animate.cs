using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    public float moveSpeed = 10f;
    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            animator.SetBool("IsWalking", true);
        }
        if (!Input.GetKey("w"))
        {
            animator.SetBool("IsWalking", false);
        }

        if (Input.GetKey("s"))
        {
            animator.SetBool("IsBackwarding", true);
            moveSpeed = 1f;
        }
        if (!Input.GetKey("s"))
        {
            animator.SetBool("IsBackwarding", false);
            moveSpeed = 10f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("IsRunning", true);
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("IsRunning", false);
        }
    }
}
