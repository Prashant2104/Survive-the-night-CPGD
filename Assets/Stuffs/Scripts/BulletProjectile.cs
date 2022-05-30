using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;
    //[SerializeField] LayerMask targetLayer = new LayerMask();

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        rb.velocity = transform.forward * speed;
        //Invoke("DisableProjectile", 5f);
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
        {
            if (hit.transform.CompareTag("Target"))
            {
                //Hit target
                //Instantiate(vfxHitGreen, transform.position, Quaternion.identity);

                hit.transform.GetComponent<ZombieController>().TakeDamage(damage);
            }
            else
            {
                //Hit something else
                //Instantiate(vfxHitRed, transform.position, Quaternion.identity);
            }
            DisableProjectile();
        }
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }
    private void DisableProjectile()
    {
        gameObject.SetActive(false);
    }
}