using System.Collections;
using UnityEngine;

public class ARDE_SideAttack : MonoBehaviour
{
    // Automatique
    private CharacterInput input = default;
    private CharacterState state = default;
    private Transform attackFrom = default;
    //

    private enum Direction
    { Neutral, Left, Right, Up, Down }
    private Direction attackDirection = default;
    private float rotZ;

    [SerializeField]
    private ARDE_CharacterLifeSystem lifeSystem = default;

    [Space(10)]
    [Header("Attack")]
    public GameObject attackSpecial = default;
    public float attackDuration = 0.5f, attackCoolDown = 1f;
    public int energieCost = 15;

    public bool haveAirTime, haveJumpForce;
    public float airTime = 1f, jumpForce = 20f;

    private void Start()
    {
        attackFrom = this.transform;
        input = GetComponentInParent<CharacterInput>();
        state = GetComponentInParent<CharacterState>();
    }

    void Update()
    {
        if (state.canAttack)
        {
            attackDirection = DetermineAttackDirection(input.stickDirectionBrut);

            if (input.attackHeavyEnter && lifeSystem.energie > energieCost && attackDirection == Direction.Left || input.attackHeavyEnter && lifeSystem.energie > energieCost && attackDirection == Direction.Right)
            {
                state.canAttack = false;

                Attack();

                StartCoroutine(activateAttackIn(attackSpecial, attackDuration, attackCoolDown));

                lifeSystem.energieAttack(energieCost);

                state.soundManager.Play("AttackSpé");
            }
        }
    }

    private void Attack()
    {
        state.isAttackingDown = true;

        attackSpecial.SetActive(true);

        if (haveJumpForce)
        {
            state.body.velocity = new Vector2(state.body.velocity.x, jumpForce);
        }

        if (haveAirTime)
        {
            StartCoroutine(AirMaintain(airTime));
        }

    }

    private Direction DetermineAttackDirection(Vector2 StickNotNormalized)
    {
        //entre 0 et 1
        float NeutralZone = 0.2f;

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

    private IEnumerator activateAttackIn(GameObject attackToDesactivate, float attackDuration, float CoolDown)
    {
        yield return new WaitForSeconds(attackDuration);

        attackToDesactivate.SetActive(false);

        state.isAttackingSide = false;

        yield return new WaitForSeconds(CoolDown - attackDuration);

        state.canAttack = true;

        yield return null;
    }

    private IEnumerator AirMaintain(float airMaintienTime)
    {

        state.body.constraints = RigidbodyConstraints2D.FreezePosition;

        yield return new WaitForSeconds(airMaintienTime);

        state.body.constraints = RigidbodyConstraints2D.None;
        state.body.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return null;
    }
}
