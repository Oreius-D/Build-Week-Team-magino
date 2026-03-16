using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    private PlayerAnimator playerAnimator;

    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerAnimator.Run();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnimator.Slide();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            playerAnimator.Hit();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            playerAnimator.Death();
        }
    }
}
