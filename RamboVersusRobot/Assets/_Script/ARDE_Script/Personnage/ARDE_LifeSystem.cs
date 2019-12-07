using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_LifeSystem : MonoBehaviour
{
    Transform mySelf = null;
    Rigidbody2D myBody = null;

    protected int health = 5;

    private void Start()
    {
        mySelf = this.GetComponent<Transform>();
        myBody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //meurt si PV = 0
        isAlive();
    }

    //Se prend des dégâts
    private void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.CompareTag("damage"))
        {
            GameObject objectAttack = hit.gameObject;
            ARDE_AttackSystem attack = objectAttack.GetComponentInParent<ARDE_AttackSystem>();

            TakeDamage(attack.damage);
            TakeKnockBack(attack.knockback, attack.mySelf);
        }

    }

    //Fonctions interne
    protected void isAlive()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
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