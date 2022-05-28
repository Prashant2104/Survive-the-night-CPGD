using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    float distance;

    private PlayerManager player;
    private Animator anim;
    public NavMeshAgent agent;
    [SerializeField] LayerMask zombies;

    [Header("Health")]
    [SerializeField] float currentHealth;
    [SerializeField] float minHealth;
    [SerializeField] float maxHealth;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerManager>();
        agent = GetComponent<NavMeshAgent>();

        currentHealth = Random.Range(minHealth, maxHealth);
        StartCoroutine(TriggerCheck());
    }
    private void Update()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        anim.SetFloat("Distance", distance);

        RaycastHit hit;
        if(Physics.Raycast((this.transform.position + new Vector3(0,1.5f,0)), Vector3.forward, out hit, 8f))
        {
            if (hit.transform.CompareTag("Player"))
                anim.SetBool("Triggered", true);
        }

        if(distance > 50f)
            anim.SetBool("Triggered", false);
    }
    IEnumerator TriggerCheck()
    {
        RaycastHit hit;
        if (Physics.SphereCast(this.transform.position, 5f, player.transform.position - this.transform.position, out hit, 10f, zombies))
        {
            if (hit.transform.GetComponent<ZombieController>().anim.GetBool("Triggered"))
                anim.SetBool("Triggered", true);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(TriggerCheck());
    }
    public GameObject GetPlayer()
    {
        return player.gameObject;
    }
    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0)
            return;
        currentHealth -= damage;
        if (currentHealth <= 0)
            anim.SetTrigger("Dead");
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay((this.transform.position + new Vector3(0, 1.5f, 0)), Vector3.forward);
    }
}