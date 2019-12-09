using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    CharacterInput input = default;
    CharacterState state = default;
    Transform attackFrom = default;
    ARDE_CharacterLifeSystem lifeSystem = default;

    [Header("Classique")]
    public GameObject attackLight = default;
    public int lightEnergieCost = 5;
    public float attackLightDuration = 0.3f;
    public Vector2 lightAttackDash = new Vector2(10f, 0f);
    [SerializeField] private float rotZ;
    [Space(10)]

    [Header("Neutral")]
    public GameObject attackHeavyNeutral = default;
    public float airNeutralTime = 1f;
    public int heavyNeutralEnergieCost = 15;
    public float attackHeavyNeutralDuration = 1.5f;
    [Space(10)]

    [Header("Side")]
    public GameObject attackHeavySide = default;
    public float airSideTime = 1f;
    public int heavySideEnergieCost = 15;
    public float attackHeavySideDuration = 1.5f;
    [Space(10)]

    [Header("Up")]
    public GameObject attackHeavyUp = default;
    public int heavyUpEnergieCost = 15;
    public float attackHeavyUpDuration = 1.5f;
    [Space(10)]

    [Header("Down")]
    public GameObject attackHeavyDown = default;
    public int heavyDownEnergieCost = 15;
    public float attackHeavyDownDuration = 1.5f;

    void Start()
    {
        attackFrom = this.transform;
        input = GetComponentInParent<CharacterInput>();
        state = GetComponentInParent<CharacterState>();
        lifeSystem = this.GetComponentInParent<ARDE_CharacterLifeSystem>();

    }

    void Update()
    {
        if (state.canAttack == true)
        {
            if (input.attackLightEnter)
            {
                defaultAttack();
            }
            else 
            if (input.attackHeavyEnter)
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

        lifeSystem.energieAttack(lightEnergieCost);

        Instantiate(attackLight, attackFrom);

        StartCoroutine(activateAttackIn(attackLightDuration));
    }

    void specialAttack()
    {
        state.canAttack = false;

        //Neutral Spécial
        if (input.stickXabs < 0.3 && input.stickYabs < 0.3)
        {
            lifeSystem.energieAttack(heavyNeutralEnergieCost);

            Instantiate(attackHeavyNeutral, attackFrom);
            StartCoroutine(AirMaintain(airNeutralTime));
            StartCoroutine(activateAttackIn(attackHeavyNeutralDuration));
        }
        //Side Spécial
        else if (input.stickXabs > 0.3 && input.stickXabs > input.stickYabs)
        {
            lifeSystem.energieAttack(heavySideEnergieCost);

            Instantiate(attackHeavySide, attackFrom);
            StartCoroutine(AirMaintain(airSideTime));
            StartCoroutine(activateAttackIn(attackHeavySideDuration));
        }
        // Up Spécial
        else if (input.stickYabs > 0.3 && input.stickYabs > input.stickXabs && input.stickY > 0)
        {
            lifeSystem.energieAttack(heavyUpEnergieCost);

            Instantiate(attackHeavyUp, attackFrom);
            StartCoroutine(activateAttackIn(attackHeavyUpDuration));
        }
        // Down Spécial
        else if (input.stickYabs > 0.3 && input.stickYabs > input.stickXabs && input.stickY < 0)
        {
            lifeSystem.energieAttack(heavyDownEnergieCost);

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
