using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMotion : MonoBehaviour
{
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;

    Vector3 moveDirection;
    public Transform cameraObject;
    Rigidbody rb;

    [Header("Falling")]
    public float InAirTimer;
    public float LeapingVelocity;
    public float FallingVelocity;
    public float rayCastHeightOffset;
    public LayerMask groundLayer;

    [Header("Actions")]
    public bool IsSprinting;
    public bool IsGrounded;
    public bool IsJumping;
    public bool IsAiming;

    [Header("Movement")]
    public float WalkSpeed;
    public float SlowRunSpeed;
    public float SprintSpeed;
    public float RotationSpeed;

    [Header("Jump")]
    public float JumpHeight;
    public float GravityIntensity;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
    }

    public void HandleAllMovement()
    {
        HandleFallingAndlanding();

        if (playerManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (IsJumping)
            return;

        moveDirection = cameraObject.forward * inputManager.VerticalInput;
        moveDirection += cameraObject.right * inputManager.HorizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (IsSprinting && !IsAiming)
        {
            moveDirection *= SprintSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.45f)
                moveDirection *= SlowRunSpeed;
            else
                moveDirection *= WalkSpeed;
        }

        Vector3 movementVelocity = moveDirection;
        rb.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if (IsJumping || IsAiming)
            return;

        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.VerticalInput;
        targetDirection += cameraObject.right * inputManager.HorizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
    private void HandleFallingAndlanding()
    {
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPos = transform.position;
        rayCastOrigin.y += rayCastHeightOffset;

        if (!IsGrounded && !IsJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.playTargerAnimation("Falling", true);
            }

            InAirTimer += Time.deltaTime;
            rb.AddForce(transform.forward * LeapingVelocity);
            rb.AddForce(-Vector3.up * FallingVelocity * InAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.1f, -Vector3.up, out RaycastHit hit, groundLayer))
        {
            if (!IsGrounded && playerManager.isInteracting)
            {
                if (InAirTimer > 0.2f && InAirTimer < 0.75f)
                    animatorManager.playTargerAnimation("Landing", true);
                else if(InAirTimer > 0.75f && InAirTimer < 1.2f)
                    animatorManager.playTargerAnimation("HardLanding", true);
                else
                    animatorManager.playTargerAnimation("LandRolling", true);
            }
            Vector3 rayCastPoint = hit.point;
            targetPos.y = rayCastPoint.y;
            IsGrounded = true;

            InAirTimer = 0;
        }
        else
        {
            IsGrounded = false;
        }

        if(IsGrounded && !IsJumping)
        {
            if(playerManager.isInteracting || inputManager.moveAmount > 0.01)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime/0.1f);
            }
            else
            {
                transform.position = targetPos;
            }
        }
    }
    public void HandleJump()
    {
        if(IsGrounded && !IsAiming)
        {
            animatorManager.animator.SetBool("isJumping", true);

            float jumpingVelocity = Mathf.Sqrt(-2 * GravityIntensity * JumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            rb.velocity = playerVelocity;

            if (inputManager.moveAmount >= 0.01)
            {
                animatorManager.playTargerAnimation("RunningJump", false);
            }
            else
            {
                animatorManager.playTargerAnimation("Jump", false);
            }
        }
    }
}