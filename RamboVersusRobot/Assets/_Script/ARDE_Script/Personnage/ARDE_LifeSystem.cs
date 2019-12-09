using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_LifeSystem : MonoBehaviour
{
    protected Transform mySelf = default;
    protected Rigidbody2D myBody = default;
    protected GameObject Me = default;

    public int health = 5;

    private void Start()
    {
        mySelf = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        Me = this.gameObject;
    }

    void Update()
    {
        //meurt si PV = 0
        isAlive(Me);
    }

    //Se prend des dégâts
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("damage"))
        {
            GameObject objectAttack = hit.gameObject;
            ARDE_AttackSystem attack = objectAttack.GetComponentInParent<ARDE_AttackSystem>();

            TakeDamage(attack.damage);
            TakeKnockBack(attack.knockback, attack.attackPos);
        }
    }

    //Fonctions interne
    protected void isAlive(GameObject Me)
    {
        if (health <= 0)
        {
            Destroy(Me);
        }
    }

    //Fonctions Accessible
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void TakeKnockBack(float knockbackPower, Transform attackSource)
    {
        // La direction de l'attaque
        Vector2 knockBackDirection = mySelf.position - attackSource.position;
        knockBackDirection.Normalize();

        // Subit le recul de l'attaque
        myBody.velocity = knockBackDirection * knockbackPower;

    }

}