using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterInput player = null;


    [Header("saut")]
    //Paramètre de saut
    [SerializeField] private float jumpForce = 20f;

    [Header("wallJump")]
    //Paramètre du wallJump
    [SerializeField] private float wallJumpForce = 25f;
    [SerializeField] private float wallJumpHeight = 20f;

    [Header("course")]
    //Paramètre de course
    [SerializeField] private float runSpeed = 2f;
    [SerializeField] private float airControl = 0.5f;
    [SerializeField] private float groundControl = 1f;

    [Header("dash")]
    [SerializeField] private Vector2 boostSpeed = new Vector2(20, 0);
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float dashCooldown = 1f;

    void FixedUpdate()
    {
        characterRun();

        characterJumpRecover();

        characterJump();

        characterWallJump();

        if (player.canDash == true && player.inputDashEnter)
        {
            StartCoroutine(Dash(dashDuration)); //Start the Coroutine called "Dash", and feed it the time we want it to boost us
        }

    }

    void characterRun()
    {
        if (player.canMove == true && player.inputStickXabs > 0)
        {
            //la course
            if (player.isOnGround == true)
            {
                player.body.velocity += new Vector2(runSpeed * player.lookingRight * player.inputStickXabs * groundControl, 0f);
            }
            else
            {
                player.body.velocity += new Vector2(runSpeed * player.lookingRight * player.inputStickXabs * airControl, 0f);
            }
        }   
    }

    void characterJump()
    {
        if (player.canJump == true && player.inputJumpEnter == true && player.isOnGround == true)
        {
            player.canJump = false;

            player.body.velocity += new Vector2(0f, jumpForce);
        }
    }

    void characterJumpRecover()
    {
        if (player.isOnGround)
        {
            player.canJump = true;
        }
    }

    void characterWallJump()
    {
        if (player.isOnWall == true && player.inputJumpEnter == true)
        {
            player.body.velocity = new Vector2(-player.lookingRight * wallJumpForce, player.body.velocity.y + wallJumpHeight);
        }
    }

    IEnumerator Dash(float dashDuration)
    {
        float time = 0f; 
        player.canDash = false;
        player.isSpeedLimit = false;
        player.body.velocity = Vector2.zero;

        while (dashDuration > time) //we call this loop every frame while our custom dashDurationation is a higher value than the "time" variable in this coroutine
        {
            time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            player.body.velocity = new Vector2(player.body.velocity.x, 0) + boostSpeed * player.lookingRight; //set our rigidbody velocity to a custom velocity every frame
            yield return 0; //go to next frame
        }

        player.isSpeedLimit = true;
        player.body.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCooldown); 
        player.canDash = true;
    }
}
