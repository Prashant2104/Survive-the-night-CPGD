using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM_Attack : ZombieFSM
{
    [SerializeField] float lightDamage;
    [SerializeField] float heavyDamage;
    [SerializeField] float biteDamage;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (animator.GetInteger("Choice") == 0 && animator.GetFloat("Distance") <= 2)
            player.GetComponent<PlayerManager>().TakeDamage(lightDamage);
        else if (animator.GetInteger("Choice") == 1 && animator.GetFloat("Distance") <= 2)
            player.GetComponent<PlayerManager>().TakeDamage(heavyDamage);
        else if (animator.GetInteger("Choice") == 2 && animator.GetFloat("Distance") <= 2)
            player.GetComponent<PlayerManager>().TakeDamage(biteDamage);
    }
}