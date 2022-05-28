using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM : StateMachineBehaviour
{
    public GameObject zombie;
    public GameObject player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        zombie = animator.gameObject;
        player = zombie.GetComponent<ZombieController>().GetPlayer();
    }
}