using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItems : MonoBehaviour
{
    PlayerManager player;
    [SerializeField] float HealthBoost;

    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.health += HealthBoost;
            if (player.health > player.maxHealth)
                player.health = player.maxHealth;

            Destroy(this.gameObject);
        }
    }
}
