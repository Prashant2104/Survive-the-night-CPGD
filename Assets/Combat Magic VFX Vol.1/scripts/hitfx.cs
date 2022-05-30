using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitfx : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float damage;
    private void Start()
    {
        StartCoroutine(Explode());   
    }
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2f);
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(expl, 3); // delete the explosion after 3 seconds
        this.gameObject.SetActive(false);
    }
    /*void OnCollisionEnter()
    {
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(expl, 3); // delete the explosion after 3 seconds
        this.gameObject.SetActive(false);
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Target"))
        {
            collision.transform.GetComponent<ZombieController>().TakeDamage(damage);
        }

        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(expl, 3); // delete the explosion after 3 seconds
        this.gameObject.SetActive(false);
    }
}