using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_LifeSystem : MonoBehaviour
{
    protected Transform mySelf = default;
    protected Rigidbody2D myBody = default;
    protected GameObject Me = default;
    public ARDE_ScreenShake cameraShake = default;
    public bool haveTakeDamage = false;
    public int health = 5;
    public ARDE_SoundManager soundManager = default;

    public GameObject heart, pointNRJ, sparkle = default;
    [Range(0,100)]
    public float heartProba = 0.3f;
    public float numberOfNRJ = 5;

    [Range(0,1)]
    public float bulletScreenShake = 0.2f, attackScreenShake = 0.4f;

    [Range(0.5f, 3)]
    public float knockbackSensitivity = 1f;


    private void Start()
    {
        mySelf = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        Me = this.gameObject;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<ARDE_SoundManager>();

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        cameraShake = cam.GetComponent<ARDE_ScreenShake>();
    }

    void Update()
    {
        //meurt si PV = 0
        isAlive(Me, 0.3f);
    }

    //Se prend des dégâts
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (!haveTakeDamage)
        {
            if (hit.CompareTag("damage"))
            {
            GameObject objectAttack = hit.gameObject;
            ARDE_AttackSystem attack = objectAttack.GetComponentInParent<ARDE_AttackSystem>();

            //S'il s'agit d'un projectile
            if (attack == null)
            {
                ARDE_Projectile bullet = objectAttack.GetComponentInParent<ARDE_Projectile>();
                TakeDamage(bullet.damage);
                TakeKnockBack(bullet.knockback, bullet.mySelf);
                cameraShake.trauma += bulletScreenShake;

                StartCoroutine(damageInvulnerabilty(0.1f));

                return;
            }

            TakeDamage(attack.damage);
            TakeKnockBack(attack.knockback, attack.attackPos);
            cameraShake.trauma += attackScreenShake;

            StartCoroutine(damageInvulnerabilty(0.1f));
            }
        }
    }



    //Fonctions interne
    protected void isAlive(GameObject Me, float DeathScrennShake)
    {
        if (health <= 0)
        {
            cameraShake.trauma += DeathScrennShake;

            for (int i = 0; i != numberOfNRJ; i++)
            {
                Instantiate(pointNRJ, mySelf.position, mySelf.rotation);
            }

            float alea;
            alea = Random.Range(0f, 100f);
            if(alea < heartProba)
            {
                Instantiate(heart, mySelf.position, mySelf.rotation);
            }


            soundManager.Play("RobotDeath");
            Destroy(Me);
        }
    }

    protected IEnumerator damageInvulnerabilty (float duration)
    {
        haveTakeDamage = true;

        yield return new WaitForSeconds(duration);

        haveTakeDamage = false;

    }

    //Fonctions Accessible
    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(sparkle, mySelf.position, mySelf.rotation, mySelf);
        soundManager.Play("RobotHit");
    }

    public void TakeKnockBack(float knockbackPower, Transform attackSource)
    {
        // La direction de l'attaque
        Vector2 knockBackDirection = mySelf.position - attackSource.position;
        knockBackDirection.Normalize();

        // Subit le recul de l'attaque
        myBody.velocity = knockBackDirection * knockbackSensitivity * knockbackPower;

    }

}