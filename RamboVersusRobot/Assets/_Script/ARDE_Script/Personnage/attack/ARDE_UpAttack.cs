using System.Collections;
using UnityEngine;

public class ARDE_UpAttack : MonoBehaviour
{
    private Direction attackRequest = Direction.Up;

    // Automatique
    public CharacterInput input = default;
    public CharacterState state = default;
    public GameObject charac;
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

            if (input.attackHeavyEnter && lifeSystem.energie > energieCost && attackDirection == attackRequest)
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
        state.isAttackingUp = true;

        attackSpecial.SetActive(true);

        if (haveJumpForce)
        {
            state.body.velocity = new Vector2(state.body.velocity.x, jumpForce);
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

        state.isAttackingUp = false;

        yield return new WaitForSeconds(CoolDown - attackDuration);

        state.canAttack = true;

        yield return null;
    }
}
