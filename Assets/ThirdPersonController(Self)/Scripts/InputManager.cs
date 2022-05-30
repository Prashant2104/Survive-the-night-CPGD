using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    ThirdPersonPlayerController playerControls;
    PlayerMotion playerMotion;
    AnimatorManager animatorManager;
    PlayerManager player;
    [SerializeField] GameManager manager;

    [SerializeField] Vector2 movementInput;
    [SerializeField] Vector2 cameraInput;

    public float moveAmount;

    public float VerticalInput;
    public float HorizontalInput;

    public bool aiming;
    public bool shootInput;

    public bool SprintInput;
    public bool JumpInput;
    public bool CanJump;

    public float cameraInputX;
    public float cameraInputY;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerMotion = GetComponent<PlayerMotion>();
        player = GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new ThirdPersonPlayerController();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprint.performed += i => SprintInput = true;
            playerControls.PlayerActions.Sprint.canceled += i => SprintInput = false;

            playerControls.PlayerActions.Jump.performed += i => JumpInput = true;

            playerControls.PlayerActions.Aim.performed += i => aiming = !aiming;
            //playerControls.PlayerActions.Aim.canceled += i => aimInput = false;

            playerControls.PlayerActions.Shoot.performed += i => shootInput = true;
            playerControls.PlayerActions.Shoot.canceled += i => shootInput = false;

            playerControls.PlayerActions.PickUp.performed += i => player.ItemPickUp();
        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    public void HandleAllInput()
    {
        HandleMovementInput();
        HandleSprintInput();
        if(CanJump)
            HandleJumpInput();
    }
    private void HandleMovementInput()
    {
        VerticalInput = movementInput.y;
        HorizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(HorizontalInput) + Mathf.Abs(VerticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerMotion.IsSprinting);
    }
    private void HandleSprintInput()
    {
        if (SprintInput && moveAmount > 0.5f)
            playerMotion.IsSprinting = true;
        else
            playerMotion.IsSprinting = false;
    }
    private void HandleJumpInput()
    {
        if (JumpInput)
        {
            JumpInput = false;
            playerMotion.HandleJump();
        }
    }
}