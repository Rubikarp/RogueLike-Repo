using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_EnnemisGround01 : ARDE_EnnemisBehavior
{
    [Header("Ground Base")]
    public GameObject attack;
    public Transform attackContainer;
    public float attackCoolDown = 0.3f;
    public bool haveAttack = false;

    
    void Update()
    {
        #region Variables

        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - transform.position);
        //calcul la distance entre le GameObject et le joueur
        playerDistance = Vector2.Distance(transform.position, player.position);

        #endregion

        PlayerRelativeToTheEnnemy(playerDistance, detectionRange, ToNearDistance, ToFarDistance);

        TrackPlayer();

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, playerDirection.normalized * detectionRange, Color.blue);
        Debug.DrawRay(transform.position, playerDirection.normalized * ToFarDistance, Color.red);
        Debug.DrawRay(transform.position, playerDirection.normalized * ToNearDistance, Color.green);
    }

    protected void TrackPlayer()
    {
        if (playerDetecting)
        {
            if (playerToNear)
            {
                if (!haveAttack)
                {
                    StartCoroutine(Attack(attackCoolDown));
                }

                myBody.velocity += new Vector2(-playerDirection.normalized.x, 0) * speed/20;
            }
            else
            if (playerToFar)
            {
                myBody.velocity += new Vector2(+playerDirection.normalized.x, 0) * speed/30;
            }
            else
            {
                if (!haveAttack)
                {
                    StartCoroutine(Attack(attackCoolDown));
                }
                myBody.velocity /= new Vector2(1.1f, 1);
            }

        }
    }

    IEnumerator Attack(float CoolDown)
    {
        Instantiate(attack, mySelf.position, mySelf.rotation, attackContainer);
        haveAttack = true;

        yield return new WaitForSeconds(CoolDown);
        haveAttack = false;

    }
}
