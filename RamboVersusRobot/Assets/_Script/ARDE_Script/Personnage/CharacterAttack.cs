using System.Collections;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    CharacterInput input = default;
    CharacterState state = default;
    Transform attackFrom = default;

    [SerializeField]
    ARDE_CharacterLifeSystem lifeSystem = default;

    enum Attack {Neutral, Side, Up, Down }
    Attack attackDirection = default;

    [Header("Classique")]
    public GameObject attackLight = default;
    public int lightEnergieCost = 1;
    public Vector2 lightAttackDash = new Vector2(10f, 0f);
    [SerializeField] private float rotZ;
    [Space(10)]

    [Header("Neutral")]
    public GameObject attackHeavyNeutral = default;
    public float airNeutralTime = 1f;
    public int heavyNeutralEnergieCost = 15;
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
    [Space(10)]

    [Header("Down")]
    public GameObject attackHeavyDown = default;
    public int heavyDownEnergieCost = 15;

    void Start()
    {
        attackFrom = this.transform;
        input = GetComponentInParent<CharacterInput>();
        state = GetComponentInParent<CharacterState>();
    }

    void Update()
    {
        if (input.attackLightEnter)
        {
            defaultAttack();
            state.soundManager.Play("AttackLight");

        }
        else
        if (input.attackHeavyEnter)
        {
            specialAttack();
            state.soundManager.Play("AttackSpé");

        }
    }

    void defaultAttack()
    {
        state.isAttackingLight = true;

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
        lifeSystem.energieAttack(lightEnergieCost);

        StartCoroutine(activateAttackIn(state.isAttackingLight));
    }

    void specialAttack()
    {
        //Neutral Spécial
        if (input.stickXabs < 0.3 && input.stickYabs < 0.3 && lifeSystem.energie > heavyNeutralEnergieCost)
        {
            state.isAttackingNeutral = true;

            lifeSystem.energieAttack(heavyNeutralEnergieCost);

            attackFrom.transform.rotation = Quaternion.Euler(0f, state.isLookingRight?0f:180f, 0f);
            Instantiate(attackHeavyNeutral, attackFrom);

            StartCoroutine(AirMaintain(airNeutralTime));

            StartCoroutine(activateAttackIn(state.isAttackingNeutral));
        }
        //Side Spécial
        else if (input.stickXabs > 0.3 && input.stickXabs > input.stickYabs && lifeSystem.energie > heavySideEnergieCost)
        {
            state.isAttackingSide = true;

            lifeSystem.energieAttack(heavySideEnergieCost);

            attackFrom.transform.rotation = Quaternion.Euler(0f, state.isLookingRight ? 0f : 180f, 0f);
            Instantiate(attackHeavySide, attackFrom);

            StartCoroutine(AirMaintain(airSideTime));

            StartCoroutine(activateAttackIn( state.isAttackingSide));
        }
        // Up Spécial
        else if (input.stickYabs > 0.3 && input.stickYabs > input.stickXabs && input.stickY > 0 && lifeSystem.energie > heavyUpEnergieCost)
        {
            state.isAttackingUp = true;

            lifeSystem.energieAttack(heavyUpEnergieCost);

            attackFrom.transform.rotation = Quaternion.Euler(0f, state.isLookingRight ? 0f : 180f, 0f);
            Instantiate(attackHeavyUp, attackFrom);

            StartCoroutine(activateAttackIn( state.isAttackingUp));
        }
        // Down Spécial
        else if (input.stickYabs > 0.3 && input.stickYabs > input.stickXabs && input.stickY < 0 && lifeSystem.energie > heavyDownEnergieCost)
        {
            state.isAttackingDown = true;

            lifeSystem.energieAttack(heavyDownEnergieCost);

            attackFrom.transform.rotation = Quaternion.Euler(0f, state.isLookingRight ? 0f : 180f, 0f);
            Instantiate(attackHeavyDown, attackFrom);

            StartCoroutine(activateAttackIn( state.isAttackingDown));
        }
    }

    IEnumerator activateAttackIn(bool attackState)
    {
        yield return new WaitForSeconds(0.3f);

        state.isAttackingLight = false;
        state.isAttackingNeutral = false;
        state.isAttackingSide = false;
        state.isAttackingUp = false;
        state.isAttackingDown = false;

    }

    IEnumerator AirMaintain(float airMaintienTime)
    {
        float time = 0f;
        
        while (time < 1f) //appel la boucle à chaque frame du dash
        {
            time += Time.deltaTime * (1/ airMaintienTime);
            state.body.velocity = new Vector2(0f, 0.9f); //boost appliqué à chaque frame
            yield return 0; //va à la prochaine frame
        }
    }

    void DetermineAttackDirection(float stickX, float stickY)
    {

    }

}
