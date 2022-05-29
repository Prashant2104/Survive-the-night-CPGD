using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_Gun : MonoBehaviour
{
    [SerializeField] bool rayShoot;

    [SerializeField] Transform bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;

    [SerializeField] float damage;
    InputManager inputManager;
    private void OnEnable()
    {
        inputManager = FindObjectOfType<InputManager>();
    }
    public void Shoot(Vector3 _mouseWorldPos, Transform _hitTrans)
    {
        if (this.gameObject == isActiveAndEnabled)
        {
            if (rayShoot)
            {
                if (_hitTrans != null)
                {
                    if (_hitTrans.CompareTag("Target"))
                    {
                        Debug.Log("hit");
                        //Hit target
                        //Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
                        _hitTrans.GetComponent<ZombieController>().TakeDamage(damage);
                    }
                    else
                    {
                        // Hit something else
                        //Instantiate(vfxHitRed, transform.position, Quaternion.identity);
                        Debug.Log("Not hit");
                    }
                    inputManager.shootInput = false;
                }
            }
            else if (!rayShoot)
            {
                Vector3 aimDIr = (_mouseWorldPos - bulletSpawnPoint.position).normalized;
                Instantiate(bulletPrefab, bulletSpawnPoint.position,
                    Quaternion.LookRotation(aimDIr, Vector3.up));
                inputManager.shootInput = false;
            }
        }
    }
}