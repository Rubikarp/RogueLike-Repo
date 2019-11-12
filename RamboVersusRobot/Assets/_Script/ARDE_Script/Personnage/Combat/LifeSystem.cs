using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public Transform me = null;
    public Rigidbody2D body = null;
    public Animation anim = null;

    public float health = 1;
    public float knockback = 100;


    private void Start()
    {
        me = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        //est-il en vie ?
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.CompareTag("damage"))
        {
            GameObject attack = hit.gameObject;
            Attack scriptAttack = attack.GetComponentInParent<Attack>();
            Transform attackFrom = scriptAttack.GetComponent<Transform>();
            Vector2 knockBack = me.position - attackFrom.position;
            knockBack.Normalize();

            anim.Play();
            body.velocity = knockBack * knockback;
            TakeDamage(scriptAttack.damage);


        }

    }



    public void TakeDamage(float damage)
    {
        health -= damage;
    }

}
