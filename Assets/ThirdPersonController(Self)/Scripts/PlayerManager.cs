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
    public GameObject playerHand;
    public HUD hud;

    public float health;

    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMotion = GetComponent<PlayerMotion>();
        anim = GetComponent<Animator>();

        inventory.ItemUsed += Inventory_ItemUsed;
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        int weaponsAvalible = playerHand.transform.childCount;
        for (int i = 0; i < weaponsAvalible; i++)
        {
            playerHand.transform.GetChild(i).gameObject.SetActive(false);
        }

        IInventoryItems item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);
        goItem.transform.parent = playerHand.transform;
        goItem.transform.localPosition = Vector3.zero;
        goItem.transform.localRotation = Quaternion.Euler(Vector3.zero);
        goItem.GetComponent<Collider>().enabled = false;
    }
    public void ItemPickUp()
    {
        if (mItemToPick != null)
        {
            inventory.AddItem(mItemToPick);
            mItemToPick.OnPickup();
            hud.CloseMessagePanel("");
        }
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

    private IInventoryItems mItemToPick = null;

    private void OnCollisionEnter(Collision collision)
    {
        IInventoryItems item = collision.collider.GetComponent<IInventoryItems>();
        if (item != null)
        {
            mItemToPick = item;

            hud.OpenMessagePanel("");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        IInventoryItems item = collision.collider.GetComponent<IInventoryItems>();
        if (item != null)
        {
            hud.CloseMessagePanel("");
            mItemToPick = null;
        }
    }
}