using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (animator.GetInteger("state") == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
        {
            animator.SetInteger("state", 1);
        }
        else if (animator.GetInteger("state") == 1 && !animator.GetCurrentAnimatorStateInfo(0).IsName("DoorClose"))
        {
            animator.SetInteger("state", 0);
        }
      
    }
}
