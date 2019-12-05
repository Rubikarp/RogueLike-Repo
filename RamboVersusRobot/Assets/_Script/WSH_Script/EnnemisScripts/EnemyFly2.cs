using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly2 : EnemyBase
{

    public float projectionForce;
    

    protected override void FixedUpdate()
    {

        distToPlayer = Vector2.Distance(transform.position, player.position);

        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - transform.position);






        //Détermine si le robot est réveillé ou non ou s'il est a porté d'attaque.

        if (distToPlayer < AttackRange + 2 && Cooldown < 0 && IsNothing == false)
        {

            IsAttacking = true;
            Attack();
            Cooldown = AttackCooldown;
            IsNothing = true;

        }
        else if (distToPlayer > detectionRange && IsNothing == false)
        {

            IsSleeping = true;
            SleepingBehaviour();

        }
        else if (distToPlayer < detectionRange && IsNothing == false && IsAttacking == false)
        {

            IsSleeping = false;
            AwakenBehaviour();

        } else if (IsNothing == true && distToPlayer > AttackRange + 5)
        {

            IsNothing = false;
            IsAttacking = false;

        } else if (IsNothing == true && Cooldown < 0)
        {

            IsNothing = false;

        }

            Cooldown -= Time.deltaTime;




    }

    protected override void AwakenBehaviour()
    {


        if (distToPlayer > (AttackRange + 2))
        {

            MoveToPosition(player, speed);
            

        } else if (distToPlayer < AttackRange)
        {

            
            MoveToPosition(player, -speed * 2);
            

        } else if (distToPlayer > AttackRange && distToPlayer < (AttackRange + 2))
        {

            MoveToPosition(transform, speed);

        }


        FacePlayer();


    }

    protected override void Attack()
    {

        srb.AddForce(new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y) * projectionForce);
        Debug.Log("Bla");

    }


    protected override void SleepingBehaviour()
    {



    }
}
