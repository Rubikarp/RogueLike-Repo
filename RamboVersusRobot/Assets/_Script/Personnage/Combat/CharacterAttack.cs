using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private CharacterInput player = null;
    [SerializeField] private Transform attackFrom = null;

    //l'attaque légère en elle même
    [SerializeField] private GameObject attackLight = null;
    //les attaques lourdes au sol
    [SerializeField] private GameObject attackHeavyNeutralAir = null, attackHeavySideAir = null, attackHeavyUpAir = null, attackHeavyDownAir = null;
    //les attaques lourdes aérienne
    [SerializeField] private GameObject attackHeavyNeutralGround = null, attackHeavySideGround = null, attackHeavyUpGround = null, attackHeavyDownGround = null;

    //paramètre
    [SerializeField] private float attackHeavyDuration = 2f, attackLightDuration = 0.3f;
    [SerializeField] private float rotZ;

    void Update()
    {

        attackApparition();

    }

    void attackApparition()
    {
        //attack light
        if (player.canAttack == true && player.inputAttackLightEnter)
        {
            //conversion en angle le l'axe du stick
            rotZ = Mathf.Atan2(player.inputStickY, player.inputStickX) * Mathf.Rad2Deg;

            player.canAttack = false;

            if(player.inputStickY == 0 && player.inputStickX == 0 && player.lookingRight < 0)
            {
                attackFrom.transform.rotation = Quaternion.Euler(0f, 0f,270f);
                player.body.velocity += new Vector2(-10, 5);
            }  if(player.inputStickY == 0 && player.inputStickX == 0) {
                attackFrom.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                player.body.velocity += new Vector2(10, 5);
            } else {
                attackFrom.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                player.body.velocity += new Vector2(player.inputStickX * 10, player.inputStickY * 5);
            }

            Instantiate(attackLight, attackFrom);

            StartCoroutine(activateAttackIn(attackLightDuration));
        }



        //attack Heavy
        if (player.canAttack == true && player.inputAttackHeavyEnter)
        {

            player.canAttack = false;

            if (player.isOnGround)
            {
                /*
                if (player.inputStickXabs < 0.3 && player.inputStickYabs < 0.3 )
                {
                    Instantiate(attackHeavyNeutralGround, attackFrom);
                } else if(player.inputStickXabs > 0.3 && player.inputStickXabs > player.inputStickYabs)
                {
                    Instantiate(attackHeavySideGround, attackFrom);

                } else if (player.inputStickYabs > 0.3 && player.inputStickYabs > player.inputStickXabs && player.inputStickY > 0)
                {
                    Instantiate(attackHeavyUpGround, attackFrom);

                } else if (player.inputStickYabs > 0.3 && player.inputStickYabs > player.inputStickXabs && player.inputStickY < 0)
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
                if (player.inputStickXabs < 0.3 && player.inputStickYabs < 0.3)
                {
                    Instantiate(attackHeavyNeutralAir, attackFrom);
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
    }

    IEnumerator activateAttackIn(float time)
    {
        if (player.canAttack == false)
        {
            yield return new WaitForSeconds(time);

            player.canAttack = true;
        }
    }
}
