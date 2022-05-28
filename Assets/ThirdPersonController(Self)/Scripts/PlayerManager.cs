using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerMotion playerMotion;
    Animator anim;

    public bool isInteracting;
    public CameraManager cameraManager;

    public InventoryManager inventory;

    public float health;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMotion = GetComponent<PlayerMotion>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        inputManager.HandleAllInput();
    }
    private void FixedUpdate()
    {
        playerMotion.HandleAllMovement();
    }
    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
        isInteracting = anim.GetBool("isInteracting");
        playerMotion.IsJumping = anim.GetBool("isJumping");
        anim.SetBool("isGrounded", playerMotion.IsGrounded);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        IInventoryItems item = collision.collider.GetComponent<IInventoryItems>();
        if (item != null)
        {
            inventory.AddItem(item);
        }
    }
}