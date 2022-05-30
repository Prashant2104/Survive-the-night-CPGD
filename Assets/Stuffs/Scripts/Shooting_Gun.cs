using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_Gun : MonoBehaviour
{
    [SerializeField] bool rayShoot;
    [SerializeField] bool arrow;
    [SerializeField] bool spear;
    [SerializeField] bool flame;

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
                //Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(aimDIr, Vector3.up));
                if (arrow)
                {
                    GameObject bullet = ObjectPool_Crossbow.SharedInstance.GetPooledObject();
                    if (bullet != null)
                    {
                        bullet.transform.position = bulletSpawnPoint.transform.position;
                        bullet.transform.rotation = Quaternion.LookRotation(aimDIr, Vector3.up);
                        bullet.SetActive(true);
                    }
                }
                else if (spear)
                {
                    GameObject bullet = ObjectPool_SpearGun.SharedInstance.GetPooledObject(); 
                    if (bullet != null)
                    {
                        bullet.transform.position = bulletSpawnPoint.transform.position;
                        bullet.transform.rotation = Quaternion.LookRotation(aimDIr, Vector3.up);
                        bullet.SetActive(true);
                    }
                }
                else if (flame)
                {
                    GameObject bullet = ObjectPool_Flamethrower.SharedInstance.GetPooledObject();
                    if (bullet != null)
                    {
                        bullet.transform.position = bulletSpawnPoint.transform.position;
                        bullet.transform.rotation = Quaternion.LookRotation(aimDIr, Vector3.up);
                        bullet.SetActive(true);
                    }
                }
                
                inputManager.shootInput = false;
            }
        }
    }
}