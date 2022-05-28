using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFSM_Picker : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("Choice", Random.Range(0, 3));
    }
}