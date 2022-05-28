using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;

    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void playTargerAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.1f);
    }

    public void UpdateAnimatorValues(float horizontalMov, float verticalMov, bool isSprinting)
    {
        float snappedHorizontal;
        float snappedVertical;

        #region Snapped Horizontal
        if (horizontalMov > 0 && horizontalMov < 0.5f)
        {
            snappedHorizontal = 0.5f;
        }
        else if(horizontalMov >= 0.5f)
        {
            snappedHorizontal = 1f;
        }
        else if(horizontalMov < 0 && horizontalMov > -0.5f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMov <= -0.5f)
        {
            snappedHorizontal = -1f;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion

        #region Snapped Verical
        if (verticalMov > 0 && verticalMov < 0.5f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMov >= 0.5f)
        {
            snappedVertical = 1f;
        }
        else if (verticalMov < 0 && verticalMov > -0.5f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMov <= -0.5f)
        {
            snappedVertical = -1f;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion

        if(isSprinting)
        {
            snappedHorizontal = horizontalMov;
            snappedVertical = 2;
        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}