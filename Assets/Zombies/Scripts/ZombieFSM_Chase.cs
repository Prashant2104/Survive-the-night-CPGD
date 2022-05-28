using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM_Chase : ZombieFSM
{
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (animator.GetInteger("Choice") == 0)
            zombie.GetComponent<ZombieController>().agent.speed = walkSpeed;
        else if (animator.GetInteger("Choice") == 1)
            zombie.GetComponent<ZombieController>().agent.speed = runSpeed;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        zombie.GetComponent<ZombieController>().agent.SetDestination(player.transform.position);
    }
}