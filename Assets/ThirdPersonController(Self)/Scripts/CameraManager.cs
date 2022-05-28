using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public InputManager inputManager;

    public Transform Target;
    public Transform CameraPivot;
    public Transform CameraTransform;

    //public LayerMask collsionLayers;

    //public float cameraCollisionOffset = 0.25f;
    //public float minCollisionOffset = 0.25f;
    //public float cameraCollisionRadius = 0.5f;
    //public Vector3 offsetCamera = new Vector3(0, 0.5f, 0);
    //public float distance = 2.5f;

    public float cameraFollowSpeed = 0.35f;
    public float cameraLookSpeed = 2f;
    public float cameraPivotSpeed = 2f;

    public float LookAngle;
    public float PivotAngle;

    public float MinPivot = -35f;
    public float MaxPivot = 35f;

    //float defaultPos;

    //Vector3 cameraVectorPosition;
    Vector3 cameraFollowVelocity = Vector3.zero;

    private void Awake()
    {
        //defaultPos = CameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        Follow();
        RotateCamera();
        //CameraCollision();
    }

    private void Follow()
    {
        Vector3 targetPos = 
            Vector3.SmoothDamp(transform.position, Target.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPos;
    }
    private void RotateCamera()
    {
        Vector3 rotation = Vector3.zero;
        Quaternion targetRotation;

        LookAngle += (inputManager.cameraInputX * cameraLookSpeed);
        PivotAngle -= (inputManager.cameraInputY * cameraPivotSpeed);

        PivotAngle = Mathf.Clamp(PivotAngle, MinPivot, MaxPivot);

        rotation = Vector3.zero;
        rotation.y = LookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = PivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        CameraPivot.localRotation = targetRotation;
    }

    /*private void CameraCollision()
    {
        float targetPos = defaultPos;
        RaycastHit hit;
        Vector3 direction = (CameraTransform.position - CameraPivot.position);
        direction.Normalize();

        if (Physics.SphereCast
            (CameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPos), collsionLayers))
        {
            float distance = Vector3.Distance(CameraPivot.position, hit.point);
            targetPos = targetPos - (distance - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPos) < minCollisionOffset)
            targetPos = targetPos - minCollisionOffset;

        cameraVectorPosition.z = Mathf.Lerp(CameraTransform.localPosition.z, targetPos, cameraFollowSpeed);
        CameraTransform.localPosition = cameraVectorPosition;
    }*/
}