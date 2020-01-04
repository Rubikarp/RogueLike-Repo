using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] Animator animator = default;
    [SerializeField] CharacterInput input = default;
    [SerializeField] CharacterState state = default;
    [SerializeField] ARDE_CharacterLifeSystem lifeSystem = default;


    [Space(30)]
    [SerializeField] ParticleSystem dashParticleLeft = default;
    [SerializeField] ParticleSystem dashParticleRight = default;
    [SerializeField] GameObject dashParticleLeftGameObject = default;
    [SerializeField] GameObject dashParticleRightGameObject = default;
    [SerializeField] ParticleSystem dustFall = default;
    [SerializeField] ParticleSystem dustJump = default;

    private void Start()
    {
        animator = this.GetComponentInParent<Animator>();
        input = this.GetComponentInParent<CharacterInput>();
        state = this.GetComponentInParent<CharacterState>();
        lifeSystem = this.GetComponentInParent<ARDE_CharacterLifeSystem>();
        dashParticleLeftGameObject = dashParticleLeft.gameObject;
        dashParticleRightGameObject = dashParticleRight.gameObject;
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

        animator.SetBool("AttackingLight", state.isAttackingLight);
        animator.SetBool("AttackingUp", state.isAttackingUp);
        animator.SetBool("AttackingDown", state.isAttackingDown);
        animator.SetBool("AttackingNeutral", state.isAttackingNeutral);
        animator.SetBool("AttackingSide", state.isAttackingSide);

        animator.SetBool("IsDashing", state.isDashing);

        animator.SetBool("IsOnWall", state.isOnWall);
        animator.SetBool("IsOnGround", state.isOnGround);

        if (state.isDashing)
        {
            if (state.isLookingRight)
            {
                dashParticleLeftGameObject.SetActive(true);
                dashParticleLeft.Play();
            }
            else if (!state.isLookingRight)
            {
                dashParticleRightGameObject.SetActive(true);
                dashParticleRight.Play();
            }
        }
        else
        {
            dashParticleLeftGameObject.SetActive(false);
            dashParticleRightGameObject.SetActive(false);
        }

    }
}
