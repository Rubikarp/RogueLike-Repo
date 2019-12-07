using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_Projectile : ARDE_EnnemisBehavior
{

    public Transform bullet = null;

    public int damage = 1;
    public float knockback = 20;

    public Vector3 initialPos;
    public float distanceMade;
    public float maxRange = 20f;


    void Start()
    {
        mySelf = this.transform;
        initialPos = mySelf.position;
    }

    void Update()
    {
        //calcul la distance entre le GameObject et le joueur
        distanceMade = Vector2.Distance(initialPos, mySelf.position);

        if(distanceMade > maxRange)
        {
            Destroy(this.gameObject);
        }



    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Quand le projectile entre en contact et si c'est le joueur, alors le joueur prend des dégâts et du knockback
        if (other.CompareTag("Player"))
        {

            other.GetComponent<ARDE_LifeSystem>().TakeDamage(damage);
            other.GetComponent<ARDE_LifeSystem>().TakeKnockBack(knockback, bullet);

            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Terrain"))
        {
            Destroy(this.gameObject);
        }
    }

    
}
