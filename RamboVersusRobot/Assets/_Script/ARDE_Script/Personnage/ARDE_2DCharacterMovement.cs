using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_2DCharacterMovement : MonoBehaviour
{
    CharacterInput input;
    CharacterState state;
    ARDE_ScreenShake cameraShake;

    [SerializeField] ARDE_CharacterLifeSystem lifeSystem = default;

    public ARDE_SoundManager soundManager = default;

    public float VelocityY;

    [Header("saut")]
    [Range(2f, 4f)] public float fallFactor = 2f;
    [Range(2f, 24f)] public float lowJumpFactor = 4f;
    public float jumpHeight = 45f;
    public float airResistance = 2f;
    public float airFrictionActivation = 2f;
    public float wallJumpForce = 35f;
    public float wallJumpHeight = 35f;
    public float wallJumpCooldown = 1f;


    [Header("course")]
    public float RunDeadZone = 1f;
    public float maxRunSpeed = 35f;
    public float runAccelerationTime = 0.3f;
    public float runDecelerationTime = 0.3f;
    [SerializeField] float runAcceleration = 0f;
    [SerializeField] float runDeceleration = 0f;
    float runAccelerationTimer = 0.0f;
    float runDecelerationTimer = 0.0f;
    float runSoundTimer = 0f;
    [SerializeField] float runSoundCadence = 0.4f;
    bool runSoundIsPlay = false;

    [Header("airControl")]
    public float maxAirSpeed = 35f;
    public float airSpeed = 0f;
    public float airDecelerationTime = 0.5f;

    [Header("WallControl")]
    public bool grabLeft = false;
    public bool grabRight = false;
    public float wallFriction = 11f;
    public float wallSlide = 45f;

    [Header("dash")]
    public Vector2 boostSpeed = new Vector2(15, 0);
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.4f;
    public float dashScreenShake = 0.2f;
    public int dashEnergieCost = 20;


    private void Start()
    {
        input = this.GetComponent<CharacterInput>();
        state = this.GetComponent<CharacterState>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<ARDE_SoundManager>();

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        cameraShake = cam.GetComponent<ARDE_ScreenShake>();
    }

    void FixedUpdate()
    {
        VelocityY = state.body.velocity.x;

        Falling();

        if (state.canMove)
        {
            if (state.isOnGround)
            {
                state.isJumping = false;

                Run();
                Jump();
            }
            else
            if (state.isOnWall)
            {
                state.isRuning = false;
                state.isJumping = false;

                GrabWall();
                characterWallJump();
            }
            else
            {
                state.isRuning = false;

                AirControl();
            }
        }

        if (state.canDash)
        {
            if (input.dashEnter && lifeSystem.energie > dashEnergieCost)
            {
                StartCoroutine(Dash(dashDuration));
            }
        }
    }

    void Run()
    {
        if (input.stickXabs > 0)
        {
            //Avatar en train de courrir
            state.isRuning = true;
            //reset décélération
            runDecelerationTimer = 0f;
            //Lerp pour calculer son accélération
            runAcceleration = Mathf.Lerp(0, maxRunSpeed, runAccelerationTimer);
            //Force appliqué sur l'avatar
            state.body.velocity = new Vector2(runAcceleration * input.lookingRight, state.body.velocity.y);
            //Le temps s'incrémente d'où l'accélération
            runAccelerationTimer += Time.deltaTime * (1 / runAccelerationTime);
        }
        else if (state.isRuning)
        {

            //reset accélération
            runAccelerationTimer = 0f;
            //Lerp pour calculer sa décélération
            runDeceleration = Mathf.Lerp(runAcceleration, 0, runDecelerationTimer);
            //Force appliqué sur l'avatar                                                                  
            state.body.velocity = new Vector2(runDeceleration * input.lookingRight, state.body.velocity.y);
            //Le temps s'incrémente d'où l'accélération
            runDecelerationTimer += Time.deltaTime * (1 / runDecelerationTime);

            if (Mathf.Abs(state.body.velocity.x) < RunDeadZone)
            {
                state.isRuning = false;
            }
        }

        if (state.isRuning)
        {
            if (!runSoundIsPlay)
            {
                runSoundIsPlay = true;
                state.soundManager.Play("Course");
            }
            else
            {
                runSoundTimer += Time.deltaTime;
                if(runSoundTimer > runSoundCadence)
                {
                    runSoundIsPlay = false;
                    runSoundTimer = 0;
                }
            }
        }
        else
        {
            runSoundTimer = 0;
        }
    }

    void Jump()
    {
        if (input.jumpEnter == true)
        {
            state.isJumping = true;

            state.body.velocity = new Vector2(state.body.velocity.x, jumpHeight);
        }
    }

    void Falling()
    {
        if (state.body.velocity.y < 0)
        {
            //Force appliquée lorsque perso tombe
            state.body.velocity += Vector2.up * Physics2D.gravity.y * (fallFactor - 1) * Time.deltaTime;

            if (Mathf.Abs(state.body.velocity.x) > airFrictionActivation)
            {
                //Friction de l'air qui le ralentit
                state.body.velocity -= Vector2.right * airResistance * input.lookingRight * Time.deltaTime;
            }
        }
        else if (state.body.velocity.y > 0 && !input.jump)
        {
            //Force appliquée lorsque perso monte & à laché le bouton de saut ( plus fort pour réduire le saut )
            state.body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpFactor - 1) * Time.deltaTime;
        }
    }

    void AirControl()
    {
        if (input.stickXabs > 0 )
        {
            //Force appliqué sur l'avatar
            state.body.velocity += new Vector2(airSpeed * input.lookingRight, 0);

            if (Mathf.Abs(state.body.velocity.x) > 25f && !state.isWallJumping)
            {
                state.body.velocity = new Vector2(maxAirSpeed * input.lookingRight, state.body.velocity.y);
            }
        }
        else
        {
            //pour que le personnage retombe droit si le stick est relaché
            state.body.velocity /= new Vector2(1.01f, 1);

        }
    }

    void GrabWall()
    {
        if(input.grab)
        { 
        //GRAB
        if (state.isOnWallLeft && !state.isWallJumping && input.stickX != 1f)
        {
            //augmente la force qd on est sur les mur
            state.body.velocity += new Vector2(-wallFriction, 0);

        }
        else if (state.isOnWallLeft && !state.isWallJumping && input.stickX == 1f)
        {
            state.body.velocity += new Vector2(airSpeed * input.lookingRight, 0);
        }

        else
        if (state.isOnWallRight && !state.isWallJumping && input.stickX != 1f)
        {
            //augmente la force qd on est sur les mur
            state.body.velocity += new Vector2(+wallFriction, 0);

        }
        else if (state.isOnWallRight && !state.isWallJumping && input.stickX == 1f)
        {
            state.body.velocity += new Vector2(airSpeed * input.lookingRight, 0);
        }
        }
        
        //Slide
        if (input.stickY < -0.2f)
        {
            //augmente la force qd on est sur les mur
            state.body.velocity = new Vector2(state.body.velocity.x, -wallSlide * input.stickYabs);
        }

    }

    void characterWallJump()
    {
        if (state.isOnWallLeft == true && input.jumpEnter == true)
        {
            state.body.velocity = new Vector2(wallJumpForce + wallFriction, wallJumpHeight);
            StartCoroutine(WallJumpCD());
        }
        else if (state.isOnWallRight == true && input.jumpEnter == true)
        {
            state.body.velocity = new Vector2(-wallJumpForce - wallFriction, wallJumpHeight);
            StartCoroutine(WallJumpCD());
        }

    }

    IEnumerator WallJumpCD()
    {
        state.isWallJumping = true;

        yield return new WaitForSeconds(0.2f);

        state.isWallJumping = false;

        yield return new WaitForSeconds(wallJumpCooldown - 0.2f);

        state.canJump = true;
    }

    IEnumerator Dash(float dashDuration)
    {
        float time = 0f;
        state.canMove = false;
        state.canDash = false;
        state.isDashing = true;
        state.body.velocity = Vector2.zero;

        cameraShake.trauma += dashScreenShake;
        lifeSystem.energieAttack(dashEnergieCost);
        state.soundManager.Play("Dash");

        while (dashDuration > time) //we call this loop every frame while our custom dashDurationation is a higher value than the "time" variable in this coroutine
        {
            time = time + Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
            state.body.velocity = new Vector2(state.body.velocity.x, 1) + boostSpeed * input.lookingRight; //set our rigidbody velocity to a custom velocity every frame
            yield return 0; //go to next frame
        }

        state.canMove = true;
        state.body.velocity = Vector2.zero;
        state.isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        state.canDash = true;
    }
}
