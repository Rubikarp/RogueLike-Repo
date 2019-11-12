using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAnimator : MonoBehaviour
{
    [SerializeField] CharacterInput input = null;
    [SerializeField] CharacterState state = null;
    [SerializeField] Animator animator = null;

    void Update()
    {
        animator.SetFloat("VelocityY", state.body.velocity.y);
        animator.SetFloat("VelocityX", state.body.velocity.x);

        animator.SetFloat("StickY", input.stickX);
        animator.SetFloat("StickX", input.stickY);
        animator.SetFloat("Running", input.stickXabs);

        animator.SetBool("JumpButton", input.jumpEnter);
        animator.SetBool("DashButton", input.dashEnter);
        animator.SetBool("attackLightButton", input.attackLightEnter);
        animator.SetBool("attackHeavyButton", input.attackHeavyEnter);

        animator.SetBool("IsOnWall", state.isOnWall);
        animator.SetBool("IsOnGround", state.isOnGround);
    }
}
