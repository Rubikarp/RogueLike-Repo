using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class ARDE_Projectile : ARDE_EnnemisBehavior
{
    public int damage = 1;
    public float knockback = 20;

    public Vector3 initialPos;
    public Vector2 shootingDir;

    public float distanceMade;
    public float maxRange = 20f;


    void Start()
    {
        mySelf = this.transform;
        initialPos = mySelf.position;
        shootingDir = playerDirection;

        FaceShootingDirection();
    }

    void Update()
    {
        FollowShootingDirection();
        DitanceLifeTime();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Quand le projectile entre en contact et si c'est le joueur, alors le joueur prend des dégâts et du knockback
        if (other.CompareTag("Player"))
        {

            other.GetComponent<ARDE_LifeSystem>().TakeDamage(damage);
            other.GetComponent<ARDE_LifeSystem>().TakeKnockBack(knockback, mySelf);

            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Terrain"))
        {
            Destroy(this.gameObject);
        }
    }

    void FaceShootingDirection()
    {
        //calcul l'angle pour faire face au joueur
        float rotZ = Mathf.Atan2(shootingDir.y, shootingDir.x) * Mathf.Rad2Deg;
        //oriente l'object pour faire face au joueur
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    void FollowShootingDirection()
    {
        myBody.velocity = shootingDir * speed;
    }

    void DitanceLifeTime()
    {
        //calcul la distance entre le GameObject et le joueur
        distanceMade = Vector2.Distance(initialPos, mySelf.position);

        if (distanceMade > maxRange)
        {
            Destroy(this.gameObject);
        }
    }
}
