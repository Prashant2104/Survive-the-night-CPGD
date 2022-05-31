using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_Gun : MonoBehaviour
{
    [SerializeField] bool rayShoot;
    [SerializeField] bool arrow;
    [SerializeField] bool spear;
    [SerializeField] bool flame;

    //[SerializeField] Transform bulletPrefab;
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
                        //GameObject vfx = Instantiate(hitVFX, _hitTrans.position, Quaternion.identity);
                        //Destroy(vfx, 1f);
                        _hitTrans.GetComponent<ZombieController>().TakeDamage(damage);
                    }
                    else
                    {
                        Debug.Log("Not hit");
                        //GameObject vfx = Instantiate(hitVFX, _hitTrans.position, Quaternion.identity);
                        //Destroy(vfx, 1f);
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