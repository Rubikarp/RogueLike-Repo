using System.Collections;
using UnityEngine;

public class ARDE_DefaultAttack : MonoBehaviour
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
    public GameObject attackLight = default;
    public Vector2 lightAttackDash = Vector2.right *10;
    public float attackDuration = 0.3f, attackCoolDown = 1f;
    public int energieCost = 15;

    //public float airTime = 1f;
    //public float Up/DownForce = 20f;

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
            if (input.attackLightEnter && lifeSystem.energie > energieCost)
            {
                state.canAttack = false;

                defaultAttack();

                StartCoroutine(activateAttackIn(attackDuration, attackCoolDown));

                lifeSystem.energieAttack(energieCost);

                state.soundManager.Play("AttackLight");
            }
        }
    }

    private void defaultAttack()
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
    }

    private IEnumerator activateAttackIn(float attackDuration, float CoolDown)
    {
        new WaitForSeconds(attackDuration);

        state.isAttackingLight = false;

        new WaitForSeconds(CoolDown - attackDuration);

        state.canAttack = true;

        yield return null;
    }

}
