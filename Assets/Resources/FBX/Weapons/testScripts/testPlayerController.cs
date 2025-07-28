using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerController : MonoBehaviour
{
    [Header("Move Variables")]
    [SerializeField]  private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;


    [Header("Gravity")]
    [SerializeField] private float gravity;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isCharacterGrounded = false;
    private Vector3 velocity = Vector3.zero;

    [Header("Animations")]

    [SerializeField] private Animator anim;

    [Header("Other variables")]
    [SerializeField] private Collider weponCollider;
    private bool isAttacking = false;



    private void Start()
    {
        GetReferences();
        InitVariables();
    }

    private void Update()
    {
        HandleIsGrounded();

        HandleJumping();
        HandleGravity();



        HandleRunning(); //a handle movement felett kell lennie
        HandleMovement();
        HandleAnimation();

        HandleAttack();

    }

    private void HandleRunning()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;

        }
    }
    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = moveDirection.normalized;
        moveDirection = transform.TransformDirection(moveDirection);

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
    private void HandleAnimation()
    {
        if(moveDirection == Vector3.zero)
        {
            anim.SetFloat("Speed", 0, 0.2f, Time.deltaTime);

        }else if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift)){
            anim.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);
        }
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift)){
            anim.SetFloat("Speed", 1f, 0.2f, Time.deltaTime);
        }
    }
    private void HandleIsGrounded()
    {
        Debug.Log("ez a távolság a földtõl "+ groundDistance);
        isCharacterGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
    }
    private void HandleGravity()
    {
        if (isCharacterGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCharacterGrounded)
        {
            if (isAttacking)
            {
                velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
            }
            else {
                anim.SetTrigger("jumpTrigger");
                velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }
    }
    private void GetReferences()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        
    }
    private void InitVariables()
    {
        moveSpeed = walkSpeed;
    }
    private void HandleAttack()
    {
        // ha attack közben ugrunk, akkor nem megy végig az animáció, és nem állitodik vissza az isattacking
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Debug.Log("elkezdtük a támadást");
            anim.SetTrigger("attackTrigger");
        }
    }
    public void EnableWeaponCollider()
    {
        isAttacking = true;
        weponCollider.enabled = true;
    }
    public void DisableWeaponCollider()
    {
        isAttacking = false;
        Debug.Log("kikapcsoltuk az isattacking statet");
        weponCollider.enabled = false;
    }
}
