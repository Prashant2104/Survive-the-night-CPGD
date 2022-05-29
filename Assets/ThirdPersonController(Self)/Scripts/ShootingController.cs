using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class ShootingController : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] GameObject aimCameraPivot;
    [SerializeField] GameObject ThirdPersonCameraPivot;
    [SerializeField] CameraManager cameraManager;

    [Header("Aiming")]
    [SerializeField] LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] Transform debugTransform;
    Vector3 mouseWorldPosition = Vector3.zero;

    [Header("Shooting")]
    Shooting_Gun shooting;
    [SerializeField] GameObject activeWeapon;
    [SerializeField] GameObject weaponHandler;
    /*[SerializeField] bool RaycastShooting;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Transform vfxHitGreen;
    [SerializeField] Transform vfxHitRed;
    [SerializeField] float damage;*/
    Transform hitTransform = null;

    [Header("IK")]
    RigBuilder rig;

    InputManager inputManager;
    PlayerMotion playerMotion;
    Animator animator;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMotion = GetComponent<PlayerMotion>();
        animator = GetComponent<Animator>();
        rig = GetComponent<RigBuilder>();
    }
    void Update()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        hitTransform = null;

        if (inputManager.aiming)
        {
            Ray ray = aimCameraPivot.GetComponentInChildren<Camera>().ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                debugTransform.position = raycastHit.point;
                mouseWorldPosition = raycastHit.point;
                hitTransform = raycastHit.transform;
            }
        }
        else
        {
            Ray ray = ThirdPersonCameraPivot.GetComponentInChildren<Camera>().ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                debugTransform.position = raycastHit.point;
                mouseWorldPosition = raycastHit.point;
                hitTransform = raycastHit.transform;
            }
        }
        AimHandling();
        ShootHandling();
    }
    private void AimHandling()
    {
        if (inputManager.aiming)
        {
            aimCameraPivot.SetActive(true);
            cameraManager.CameraPivot = aimCameraPivot.transform;
            cameraManager.CameraTransform = aimCameraPivot.transform.GetChild(0);
            cameraManager.MaxPivot = 15;
            cameraManager.MinPivot = -7.5f;

            playerMotion.IsAiming = true;

            animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));
            animator.SetLayerWeight(1, 0f);

            rig.enabled = true;

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 10f);
        }
        else
        {
            aimCameraPivot.SetActive(false);
            cameraManager.CameraPivot = ThirdPersonCameraPivot.transform;
            cameraManager.CameraTransform = ThirdPersonCameraPivot.transform.GetChild(0);
            cameraManager.MaxPivot = 35;
            cameraManager.MinPivot = -15;

            playerMotion.IsAiming = false;

            animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
            animator.SetLayerWeight(1, 1f);

            rig.enabled = false;
        }
    }
    private void ShootHandling()
    {
        if (weaponHandler.transform.hasChanged)
        {
            for (int i = 0; i < weaponHandler.transform.childCount; i++)
            {
                if (weaponHandler.transform.GetChild(i).gameObject == isActiveAndEnabled)
                {
                    activeWeapon = weaponHandler.transform.GetChild(i).gameObject;
                    shooting = activeWeapon.GetComponent<Shooting_Gun>();
                }
            }
        }
        if (activeWeapon != null && activeWeapon == isActiveAndEnabled)
        {
            if (inputManager.shootInput && inputManager.aiming)
            {
                shooting.Shoot(mouseWorldPosition, hitTransform);
            }
        }
    }
}