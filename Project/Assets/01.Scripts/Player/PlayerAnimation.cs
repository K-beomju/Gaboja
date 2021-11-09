using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private readonly int hashMove = Animator.StringToHash("isRun");
    private readonly int hashAttack = Animator.StringToHash("isAtk");

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Ray(RaycastHit2D raycast)
    {
        if(raycast)
        {
            animator.SetBool(hashMove, false);
            animator.SetBool(hashAttack, true);
        }
        else
        {
            animator.SetBool(hashMove,true);
            animator.SetBool(hashAttack, false);
        }
    }
}
