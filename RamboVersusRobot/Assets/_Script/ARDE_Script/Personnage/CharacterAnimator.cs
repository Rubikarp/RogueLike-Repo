using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] Animator animator = default;
    [SerializeField] CharacterInput input = default;
    [SerializeField] CharacterState state = default;
    [SerializeField] ARDE_CharacterLifeSystem lifeSystem = default;

    private void Start()
    {
        animator = this.GetComponentInParent<Animator>();
        input = this.GetComponentInParent<CharacterInput>();
        state = this.GetComponentInParent<CharacterState>();
        lifeSystem = this.GetComponentInParent<ARDE_CharacterLifeSystem>();
    }

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
