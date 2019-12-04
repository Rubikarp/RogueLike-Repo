using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly1 : EnemyBase
{


    public GameObject projectile;
    

    public CircleCollider2D myCollider = null;
    public LayerMask TerrainLayerMask;
    [SerializeField] private float EnviroDetectDist = 5f;
    [SerializeField] private float EnviroflyForce = 1f;


    protected override void AwakenBehaviour()
    {

        if (distToPlayer < AttackRange)
        {

            MoveToPosition(player, -speed);
            //DontCrash();

        }
        else if (distToPlayer > AttackRange && distToPlayer < AttackRange + 2)
        {

            MoveToPosition(transform, speed);

        } else
        {

            MoveToPosition(player, speed);

        }


        FacePlayer();


    }

    protected override void Attack()
    {

        Instantiate(projectile, transform.position, transform.rotation);

    }

    protected override void SleepingBehaviour()
    {

       

    }

    /*
    void DontCrash()
    {
        RaycastHit2D ground = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, EnviroDetectDist, TerrainLayerMask);
        RaycastHit2D wallLeft = Physics2D.Raycast(myCollider.bounds.center, Vector2.left, EnviroDetectDist / 2, TerrainLayerMask);
        RaycastHit2D wallRight = Physics2D.Raycast(myCollider.bounds.center, Vector2.right, EnviroDetectDist / 2, TerrainLayerMask);
        RaycastHit2D ceilling = Physics2D.Raycast(myCollider.bounds.center, Vector2.up, EnviroDetectDist, TerrainLayerMask);

        if (ground) { srb.velocity += new Vector2(0, EnviroflyForce); }
        if (wallLeft) { srb.velocity += new Vector2(EnviroflyForce, 0); }
        if (wallRight) { srb.velocity += new Vector2(-EnviroflyForce, 0); }
        if (ceilling) { srb.velocity += new Vector2(0, -EnviroflyForce); }

    }
    */

}
