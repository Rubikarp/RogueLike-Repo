using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_HelicoMouvement : MonoBehaviour
{
    CharacterInput input;
    CharacterState state;
    ARDE_ScreenShake cameraShake;

    public ARDE_SoundManager soundManager = default;

    public float VelocityY;

    [Header("saut")]
    [Range(2f, 4f)] public float fallFactor = 2f;
    [Range(2f, 24f)] public float lowJumpFactor = 4f;
    public float airResistance = 2f;
    public float airFrictionActivation = 2f;


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

    private void Start()
    {
        input = this.GetComponent<CharacterInput>();
        state = this.GetComponent<CharacterState>();

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
            }
            else
            if (state.isOnWall)
            {
                state.isRuning = false;
                state.isJumping = false;
            }
            else
            {
                state.isRuning = false;

                AirControl();
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
}
