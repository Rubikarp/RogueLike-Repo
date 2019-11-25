using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLifeSystem : MonoBehaviour
{
    public Transform me = null;
    public Rigidbody2D body = null;
    ///public Animation anim = null;

    public float health = 1;
    public float knockback = 100;


    private void Start()
    {
        me = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        ///anim = GetComponent<Animation>();
    }

    void Update()
    {
        //Tester si l'objet meurt ou pas.
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //A priori, je n'utilise plus cette fonction, mais je l'est gardée au cas ou certaines objets l'utilisent encore,
    // si ce n'est plus cas, suffira de supprimer ce passage.
    private void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.CompareTag("damage"))
        {
            GameObject attack = hit.gameObject;
            Attack scriptAttack = attack.GetComponentInParent<Attack>();
            Transform attackFrom = scriptAttack.GetComponent<Transform>();
            Vector2 knockBack = me.position - attackFrom.position;
            knockBack.Normalize();

            
            body.velocity = knockBack * knockback;
            TakeDamage(scriptAttack.damage);


        }

    }

    //Fonction appelée pour jouer l'animation de dégâts (s'il y en a une).
    public virtual void playDamageAnim()
    {



    }


    //Fonction Appelée pour infliger un knockback
    public void TakeKnockBack(float Knockback, Transform attackFrom)
    {

        Vector2 knockBack = me.position - attackFrom.position;
        knockBack.Normalize();
        body.velocity = knockBack * knockback;

    }

    //Fonctions Appelée par les attaques pour infliger des dommages aux pv.
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

}
