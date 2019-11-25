using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] CharacterInput input = default;
    [SerializeField] CharacterState state = default;

    [Header("Attaque")]
    [SerializeField] private Transform attackFrom = default;

    [HeaderAttribute("Classique")]
    [SerializeField] private GameObject attackLight = default;
    [SerializeField] private float rotZ;
    [SerializeField] private Vector2 lightAttackDash = new Vector2(10f,0f);
    [SerializeField] private float attackLightDuration = 0.3f;

    [HeaderAttribute("Spécial")]
    [HeaderAttribute("Neutral")]
    [SerializeField] private GameObject attackHeavyNeutral = default;
    [SerializeField] private float airNeutralTime = 1f;
    [SerializeField] private float attackHeavyNeutralDuration = 1.5f;

    [HeaderAttribute("Side")]
    [SerializeField] private GameObject attackHeavySide = default;
    [SerializeField] private float airSideTime = 1f;
    [SerializeField] private float attackHeavySideDuration = 1.5f;

    [HeaderAttribute("Up")]
    [SerializeField] private GameObject attackHeavyUp = default;
    [SerializeField] private float attackHeavyUpDuration = 1.5f;

    [HeaderAttribute("Down")]
    [SerializeField] private GameObject attackHeavyDown = default;
    [SerializeField] private float attackHeavyDownDuration = 1.5f;



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
        state.canAttack = false;

        //conversion en angle le l'axe du stick
        rotZ = Mathf.Atan2(input.stickY, input.stickX) * Mathf.Rad2Deg;

        //si le stick est immobile
        if (input.stickY == 0 && input.stickX == 0)
        {
            //s'il regarde à gauche
            if (input.lookingRight < 0)
            {
                attackFrom.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 180f);
                state.body.velocity += new Vector2(-lightAttackDash.x, lightAttackDash.y);
            }
            //s'il regarde à droite
            else
            {
                attackFrom.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                state.body.velocity += lightAttackDash;
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

        //Neutral Spécial
        if (input.stickXabs < 0.3 && input.stickYabs < 0.3)
        {
            Instantiate(attackHeavyNeutral, attackFrom);
            StartCoroutine(AirMaintain(airNeutralTime));
            StartCoroutine(activateAttackIn(attackHeavyNeutralDuration));
        }
        //Side Spécial
        else if (input.stickXabs > 0.3 && input.stickXabs > input.stickYabs)
        {
            Instantiate(attackHeavySide, attackFrom);
            StartCoroutine(AirMaintain(airSideTime));
            StartCoroutine(activateAttackIn(attackHeavySideDuration));
        }
        // Up Spécial
        else if (input.stickYabs > 0.3 && input.stickYabs > input.stickXabs && input.stickY > 0)
        {
            Instantiate(attackHeavyUp, attackFrom);
            StartCoroutine(activateAttackIn(attackHeavyUpDuration));
        }
        // Down Spécial
        else if (input.stickYabs > 0.3 && input.stickYabs > input.stickXabs && input.stickY < 0)
        {
            Instantiate(attackHeavyDown, attackFrom);
            StartCoroutine(activateAttackIn(attackHeavyDownDuration));
        }
    }

    IEnumerator activateAttackIn(float time)
    {
        yield return new WaitForSeconds(time);
        state.canAttack = true;
    }

    IEnumerator AirMaintain(float airMaintienTime)
    {
        float time = 0f;
        
        while (time < 1f) //appel la boucle à chaque frame du dash
        {
            time += Time.deltaTime * (1/ airMaintienTime);
            state.body.velocity = new Vector2(0f, 1f); //boost appliqué à chaque frame
            yield return 0; //va à la prochaine frame
        }
    }
}
