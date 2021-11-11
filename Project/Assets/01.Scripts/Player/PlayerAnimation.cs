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
            GameManager.Instance.BackMove(false);

            if(Merge.Instance.bSword)
            {
                animator.SetBool(hashAttack, true);
            }

                animator.SetBool(hashMove, false);
        }
        else
        {
            GameManager.Instance.BackMove(true);
            animator.SetBool(hashMove,true);
            animator.SetBool(hashAttack, false);
        }
    }

    public void IdleTest()
    {
        if(Merge.Instance.IdleIsHave())
        {
            animator.SetBool(hashAttack, true);
        }
    }


}
