using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnimator;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void Run()
    {
        playerAnimator.SetBool("isRunning", true);
    }

    public void Jump()
    {
        playerAnimator.SetTrigger("jump");
    }

    public void Slide()
    {
        playerAnimator.SetTrigger("slide");
    }

    public void Hit()
    {
        playerAnimator.SetTrigger("hit");
    }

    public void Death()
    {
        playerAnimator.SetBool("isDead", true);
    }
}
