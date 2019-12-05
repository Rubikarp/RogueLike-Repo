using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround1 : EnemyBase
{

    


    protected override void AwakenBehaviour()
    {

        HoriMoveToPosition(player, speed);

    }

    protected override void Attack()
    {

        if (player.position.x - transform.position.x <= 0)
        {

            Debug.Log("Attaque Gauche");
            atkxoffset -= 2;

        } else if (player.position.x - transform.position.x >= 0)
        {


            Debug.Log("Attaque Droite");
            atkxoffset = 0;

        }

        Collider2D[] ennemiesToDamage = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + atkposx + atkxoffset, transform.position.y + atkposy + atkyoffset), AttackRange, Player);
        for (int i = 0; i < ennemiesToDamage.Length; i++)
        {

            ennemiesToDamage[i].GetComponent<PlayerLifeSystem>().TakeDamage(AttackDamage);
            ennemiesToDamage[i].GetComponent<PlayerLifeSystem>().TakeKnockBack(Knockback, transform);


        }

    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + atkposx, transform.position.y + atkposy), AttackRange);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y), detectionRange);
    }
}
