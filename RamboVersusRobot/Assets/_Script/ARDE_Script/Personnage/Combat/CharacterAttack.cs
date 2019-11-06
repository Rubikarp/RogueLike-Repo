using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] CharacterInput input = null;
    [SerializeField] CharacterState state = null;

    [Header("Attaque")]
    [SerializeField] private Transform attackFrom = null;

    [HeaderAttribute("Classique")]
    [SerializeField] private GameObject attackLight = null;
    
    [HeaderAttribute("Grounded")]
    [SerializeField] private GameObject attackHeavyNeutralGround = null;
    [SerializeField] private GameObject attackHeavySideGround = null;
    [SerializeField] private GameObject attackHeavyUpGround = null;
    [SerializeField] private GameObject attackHeaattackHeavyDownGroundvyDownAir = null;

    [HeaderAttribute("Aerial")]
    [SerializeField] private GameObject attackHeavyNeutralAir = null;
    [SerializeField] private GameObject attackHeavySideAir = null;
    [SerializeField] private GameObject attackHeavyUpAir = null;
    [SerializeField] private GameObject attackHeavyDownAir = null;

    [HeaderAttribute("Recovery Time")]
    [SerializeField] private float attackLightDuration = 0.3f;
    [SerializeField] private float attackHeavyDuration = 1.5f;

    public Vector2 groundHeavyUp = new Vector2(0, 30);

    [SerializeField] private float rotZ;

    [SerializeField] private float lightAttackDash = 10f;

    //Up B Ground
    [SerializeField] private float AirDuration = 1f;


    void Update()
    {
        if (state.canAttack == true)
        {
            if (input.attackLightEnter)
            {
                defaultAttack();
            }else if (input.attackHeavyEnter)
            {
                specialAttack();
            }

        }
    }

    void defaultAttack()
    {
        //conversion en angle le l'axe du stick
        rotZ = Mathf.Atan2(input.stickY, input.stickX) * Mathf.Rad2Deg;

        state.canAttack = false;

        //si le stick est immobile
        if (input.stickY == 0 && input.stickX == 0)
        {
            //s'il regarde à gauche
            if (input.lookingRight < 0)
            {
                attackFrom.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 180f);
                state.body.velocity += new Vector2(-10, 5);
            }
            else
            {
                attackFrom.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                state.body.velocity += new Vector2(10, 5);
            }
        }
        else  
        {
            attackFrom.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            state.body.velocity += input.stickDirection * lightAttackDash;
        }

        Instantiate(attackLight, attackFrom);

        StartCoroutine(activateAttackIn(attackLightDuration));
    }

    void specialAttack()
    {
        state.canAttack = false;

        if (state.isOnGround)
        {
            /*
            if (player.inputStickXabs < 0.3 && player.inputStickYabs < 0.3 )
            {
                Instantiate(attackHeavyNeutralGround, attackFrom);
            } else if(player.inputStickXabs > 0.3 && player.inputStickXabs > player.inputStickYabs)
            {
                Instantiate(attackHeavySideGround, attackFrom);

            } else */
            if (input.stickYabs > 0.3 && input.stickYabs > input.stickXabs && input.stickY > 0)
            {
                //Instantiate(attackHeavyUpGround, attackFrom);
                state.body.velocity += groundHeavyUp;

            }
            /*else if (player.inputStickYabs > 0.3 && player.inputStickYabs > player.inputStickXabs && player.inputStickY < 0)
            {
                Instantiate(attackHeavyDownGround, attackFrom);

            } else
            {
                Instantiate(attackHeavyNeutralGround, attackFrom);
            }
            */
        }
        else
        {
            if (input.stickXabs < 0.3 && input.stickYabs < 0.3)
            {
                //Instantiate(attackHeavyNeutralAir, attackFrom);
                StartCoroutine(AirMaintain(AirDuration));
            }
            /*
            else if (player.inputStickXabs > 0.3 && player.inputStickXabs > player.inputStickYabs)
            {
                Instantiate(attackHeavySideAir, attackFrom);

            }
            else if (player.inputStickYabs > 0.3 && player.inputStickYabs > player.inputStickXabs && player.inputStickY > 0)
            {
                Instantiate(attackHeavyUpAir, attackFrom);

            }
            else if (player.inputStickYabs > 0.3 && player.inputStickYabs > player.inputStickXabs && player.inputStickY < 0)
            {
                Instantiate(attackHeavyDownAir, attackFrom);

            }
            else
            {
                Instantiate(attackHeavyNeutralAir, attackFrom);
            }
            */
        }

        StartCoroutine(activateAttackIn(attackHeavyDuration));

    }

    /*void HeavyUpGround()
    {
        if (input.stickYabs > 0.3 && input.stickYabs > input.stickXabs && input.stickY > 0)
        {
            //Instantiate(attackHeavyUpGround, attackFrom);
            state.body.velocity += groundHeavyUp;

        }
    }
    */

    IEnumerator activateAttackIn(float time)
    {
        if (state.canAttack == false)
        {
            yield return new WaitForSeconds(time);

            state.canAttack = true;
        }
    }

    IEnumerator AirMaintain(float AirDuration)
    {
        float time = 0f;
        
        while (AirDuration > time) //appel la boucle à chaque frame du dash
        {
            time += Time.deltaTime;
            state.body.velocity = new Vector2(0f, 1f); //boost appliqué à chaque frame
            yield return 0; //va à la prochaine frame
        }
    }
}
