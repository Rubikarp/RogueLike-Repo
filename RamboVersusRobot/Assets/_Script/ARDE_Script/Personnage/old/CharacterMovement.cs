using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterInput input;
    CharacterState state;

    [Header("saut")]
    [SerializeField] private float jumpForce = 45f;
    [SerializeField] private float wallJumpForce = 35f;
    [SerializeField] private float wallJumpHeight = 35f;
    [SerializeField] private float ceillingJumpForce = 20f;

    [Header("course")]
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float airFriction = 6f;
    [SerializeField] private float wallFriction = 2f;
    [SerializeField] private float ceillingFriction = 2.5f;

    [Header("dash")]
    [SerializeField] private Vector2 boostSpeed = new Vector2(15, 0);
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 0.8f;

    [Header("Speed")]
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;
    [SerializeField] private float maxSpeedX = 35f;
    [SerializeField] private float maxSpeedY = 50f;


    private void Awake()
    {
        input = this.GetComponent<CharacterInput>();
        state = this.GetComponent<CharacterState>();
    }

    void FixedUpdate()
    {
        AirAndWallControl();

        characterRun();

        characterJumpRecover();

        characterJump();

        characterWallJump();

        characterCeillingJump();

        if (state.canDash == true && input.dashEnter)
        {
            StartCoroutine(Dash(dashDuration)); //Start the Coroutine called "Dash", and feed it the time we want it to boost us
        }

        speedX = state.body.velocity.x;
        speedY = state.body.velocity.y;

        if (state.isSpeedLimit)
        {
            speedLimitationX(maxSpeedX);
        }

        speedLimitationY(maxSpeedY);
    }

    void AirAndWallControl()
    {
        if (!state.isOnGround && !state.isDashing)
        {
            if (!state.isOnWall && !state.isOnCeilling)
            {
                //ralentit la vitesse aérienne
                state.body.velocity -= new Vector2(state.body.velocity.x, 0 ) / airFriction;
            }
            else if ( state.isOnWall )
            {
                //augmente la force qd on est sur les mur
                state.body.velocity += new Vector2(wallFriction * input.lookingRight, 0);
            }
            else if (state.isOnCeilling)
            {
                state.body.velocity += new Vector2(0, ceillingFriction);
                state.body.velocity /= new Vector2(1 + airFriction / 10, 1);

            }
        }
            
    }

    void characterRun()
    {
        if (state.canRun && input.stickXabs > 0)
        {
            //la course
            state.body.velocity += new Vector2(runSpeed * input.lookingRight, 0f);

        }
    }

    void characterJump()
    {
        if (state.canJump == true && input.jumpEnter == true && state.isOnGround == true)
        {
            state.canJump = false;
            state.isJumping = true;

            state.body.velocity = new Vector2(state.body.velocity.x / Mathf.Sqrt(2), jumpForce);

        }
    }

    void characterJumpRecover()
    {
        if (state.isOnGround)
        {
            state.canJump = true;
        }

        if(state.isOnGround || state.isOnWall || state.isOnCeilling)
        {
            state.isJumping = false;
        }

    }

    void characterWallJump()
    {
        if (state.isOnWallLeft == true && input.jumpEnter == true)
        {
            state.body.velocity = new Vector2( wallJumpForce + wallFriction,  wallJumpHeight);
        }
        else if (state.isOnWallRight == true && input.jumpEnter == true)
        {
            state.body.velocity = new Vector2( -wallJumpForce - wallFriction,  wallJumpHeight);
        }

    }

    void characterCeillingJump()
    {
        if (state.isOnCeilling == true && input.jumpEnter == true)
        {
            state.body.velocity = new Vector2(state.body.velocity.x / 1.42f, state.body.velocity.y) + new Vector2( input.stickX, input.stickY) * ceillingJumpForce;
        }
    }

    private void speedLimitationX(float maxSpeedX)
    {
        if (state.body.velocity.x >= maxSpeedX)
        {
            state.body.velocity = new Vector2(maxSpeedX, state.body.velocity.y);
        }

        if (state.body.velocity.x <= -maxSpeedX)
        {
            state.body.velocity = new Vector2(-maxSpeedX, state.body.velocity.y);
        }
    }

    private void speedLimitationY(float maxSpeedY)
    {
        if (state.body.velocity.y >= maxSpeedY)
        {
            state.body.velocity = new Vector2(state.body.velocity.x, maxSpeedY);
        }

        if (state.body.velocity.y <= -maxSpeedY)
        {
            state.body.velocity = new Vector2(state.body.velocity.x, -maxSpeedY);
        }
    }

    IEnumerator Dash(float dashDuration)
    {
        float time = 0f;
        state.canDash = false;
        state.isDashing = true;
        state.isSpeedLimit = false;
        state.body.velocity = Vector2.zero;

        while (dashDuration > time) //we call this loop every frame while our custom dashDurationation is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            state.body.velocity = new Vector2(state.body.velocity.x, 0) + boostSpeed * input.lookingRight; //set our rigidbody velocity to a custom velocity every frame
            yield return 0; //go to next frame
        }

        state.isSpeedLimit = true;
        state.body.velocity = Vector2.zero;
        state.isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        state.canDash = true;
    }
}
