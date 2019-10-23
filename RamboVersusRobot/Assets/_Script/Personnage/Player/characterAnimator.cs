using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAnimator : MonoBehaviour
{
    [SerializeField] private CharacterInput player = null;
    [SerializeField] private Animator animator = null;

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("VelocityY", player.body.velocity.y);
        animator.SetFloat("VelocityX", player.body.velocity.x);

        animator.SetFloat("StickY", player.inputStickX);
        animator.SetFloat("StickX", player.inputStickY);
        animator.SetFloat("Running", player.inputStickXabs);

        animator.SetBool("JumpButton", player.inputJumpEnter);
        animator.SetBool("DashButton", player.inputDashEnter);
        animator.SetBool("attackLightButton", player.inputAttackLightEnter);
        animator.SetBool("attackHeavyButton", player.inputAttackHeavyEnter);

        animator.SetBool("IsOnWall", player.isOnWall);
        animator.SetBool("IsOnGround", player.isOnGround);
    }
}
