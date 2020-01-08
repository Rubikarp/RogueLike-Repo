using System.Collections;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private CharacterInput input = default;
    private CharacterState state = default;
    private Transform attackFrom = default;

    [SerializeField]
    private ARDE_CharacterLifeSystem lifeSystem = default;

    private enum Direction
    { Neutral, Left, Right, Up, Down }
    private Direction attackDirection = default;

    public int lightEnergieCost = 1;
    public int heavyEnergieCost = 15;

    [Header("Classique")]
    public GameObject attackLight = default;
    public Vector2 lightAttackDash = new Vector2(10f, 0f);

    [Space(10)]
    [Header("Neutral")]
    public GameObject attackHeavyNeutral = default;
    public float airNeutralTime = 1f;

    [Space(10)]
    [Header("Side")]
    public GameObject attackHeavySide = default;
    public float airSideTime = 1f;
    public float airSideDuration = 1.5f;

    [Space(10)]
    [Header("Up")]
    public GameObject attackHeavyUp = default;
    public float UpJump = 20f;

    [Space(10)]
    [Header("Down")]
    public GameObject attackHeavyDown = default;
    public float DownFall = 20f;

    private void Start()
    {
        attackFrom = this.transform;
        input = GetComponentInParent<CharacterInput>();
        state = GetComponentInParent<CharacterState>();

    }

    private void Update()
    {
        if (state.canAttack)
        {
            if (input.attackLightEnter && lifeSystem.energie > lightEnergieCost)
            {
                state.canAttack = false;

                defaultAttack();

                lifeSystem.energieAttack(lightEnergieCost);

                state.soundManager.Play("AttackLight");
            }
            else
            if (input.attackHeavyEnter && lifeSystem.energie > heavyEnergieCost)
            {
                state.canAttack = false;

                specialAttack();

                StartCoroutine(activateAttackIn());

                lifeSystem.energieAttack(heavyEnergieCost);

                state.soundManager.Play("AttackSpé");
            }
        }
    }

    private void defaultAttack()
    {
        state.isAttackingLight = true;

        //conversion en angle le l'axe du stick
        float rotZ = Mathf.Atan2(input.stickY, input.stickX) * Mathf.Rad2Deg;

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

        StartCoroutine(activateAttackIn());
    }

    private void specialAttack()
    {
        attackFrom.transform.rotation = Quaternion.Euler(0f, state.isLookingRight ? 0f : 180f, 0f);

        attackDirection = DetermineAttackDirection(input.stickDirectionBrut);

        switch (attackDirection)
        {
            case Direction.Neutral:
                spacialNeutral();
                break;

            case Direction.Left:
                spacialSide();
                break;

            case Direction.Right:
                spacialSide();
                break;

            case Direction.Up:
                spacialUp();
                break;

            case Direction.Down:
                spacialDown();
                break;
        }

    }

    private void spacialUp()
    {
        state.isAttackingUp = true;

        state.body.velocity = new Vector2(state.body.velocity.x, UpJump);

        attackHeavyUp.SetActive(true);
    }

    private void spacialDown()
    {
        state.isAttackingDown = true;

        state.body.velocity = new Vector2(state.body.velocity.x, -DownFall);

        attackHeavyDown.SetActive(true);
    }

    private void spacialSide()
    {
        state.isAttackingSide = true;

        AirMaintain(airSideDuration);

        attackHeavySide.SetActive(true);
    }

    private void spacialNeutral()
    {
        state.isAttackingNeutral = true;

        AirMaintain(airNeutralTime);

        attackHeavyNeutral.SetActive(true);
    }

    private Direction DetermineAttackDirection(Vector2 StickNotNormalized)
    {
        //entre 0 et 1
        float NeutralZone = 0.5f;

        if (StickNotNormalized.normalized.magnitude < NeutralZone)
        {
            return Direction.Neutral;
        }
        else
        if (StickNotNormalized.y > Mathf.Abs(StickNotNormalized.x))
        {
            return Direction.Up;
        }
        else
        if (-StickNotNormalized.y > Mathf.Abs(StickNotNormalized.x))
        {
            return Direction.Down;
        }
        else
        if (0 < StickNotNormalized.x)
        {
            return Direction.Right;
        }
        else
        {
            return Direction.Left;
        }
    }

    private IEnumerator activateAttackIn()
    {
        new WaitForSeconds(0.3f);

        attackHeavyUp.      SetActive(false);
        attackHeavyDown.    SetActive(false);
        attackHeavyNeutral. SetActive(false);
        attackHeavySide.    SetActive(false);

        state.isAttackingLight = false;
        state.isAttackingNeutral = false;
        state.isAttackingSide = false;
        state.isAttackingUp = false;
        state.isAttackingDown = false;

        state.canAttack = true;

        yield return null;
    }

    private IEnumerator AirMaintain(float airMaintienTime)
    {

        state.body.constraints = RigidbodyConstraints2D.FreezePosition;

        new WaitForSeconds(airMaintienTime);

        state.body.constraints = RigidbodyConstraints2D.None;
        state.body.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return null;
    }

}