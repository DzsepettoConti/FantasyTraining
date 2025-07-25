using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool inAir;
    public float speed = 5f;
    public float gravity = -25f;
    public float jumpHeight = 1f;
    public  Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        bool wasGrounded = isGrounded;
        isGrounded = controller.isGrounded;

        if (!wasGrounded && isGrounded)
        {
            animator.SetBool("Jump", false); // földetéréskor állítsd false-ra
        }
    }
    //Recive the inputs for our inputManager.cs and apply them to our character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            inAir = true;
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
            animator.SetBool("Jump", inAir);
        }
    }
}
