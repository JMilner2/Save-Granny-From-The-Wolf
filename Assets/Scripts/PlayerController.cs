using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;
    [SerializeField] float jumpForce = 2.0f;
    [Header("Ground Check")]
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffSet;
    [SerializeField] LayerMask groundLayer;

    bool isGrounded;

    float ySpeed;
    Quaternion targerRotation;
    Vector3 jump;
    public Vector3 gravity = Physics.gravity;
    private bool jumping = false;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;

    private void Awake()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

        var moveInput = (new Vector3(h, 0, v)).normalized;

        var moveDir = cameraController.PlanarRotation * moveInput;



        GroundCheck();
        if (!isGrounded)
        {
            ySpeed += gravity.y * Time.deltaTime;  //if not grounded applies local gravity to player
        }
        else
        {
            if (jumping)
            {
                ySpeed = jumpForce; //if grounded player can jump 
                jumping = false;

            }
            else
            {
                ySpeed = -0.5f; //if not grounded and not jumping apply very small downwards force
            }
        }

        var velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime); //moves the player

        if (moveAmount > 0)
        {
            targerRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targerRotation,
            rotationSpeed * Time.deltaTime);

        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
    }

    private void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))   //checks if player can jump
        {
            jumping = true;
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(transform.TransformPoint(groundCheckOffSet), groundCheckRadius, groundLayer); //checks if player is on the ground
    }

    private void OnDrawGizmosSelected() //draws sphere colliders to see where grounded is being checked
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffSet), groundCheckRadius);
    }

}
