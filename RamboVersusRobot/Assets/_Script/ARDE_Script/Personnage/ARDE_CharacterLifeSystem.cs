using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_CharacterLifeSystem : ARDE_LifeSystem
{
    public GameObject player = default;
    [SerializeField]
    new Rigidbody2D myBody;

    // health already define
    private int maxHealth = 7;

    [Space(10)]
    public int energie = 80;
    public int energieParSec = 3;
    private int maxEnergie = 100;

    float time = 0f;

    void Start()
    {
        mySelf = this.transform;

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        cameraShake = cam.GetComponent<ARDE_ScreenShake>();

    }

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

                StartCoroutine(damageInvulnerabilty(0.2f));

                return;
            }

            TakeDamage(attack.damage);
            TakeKnockBack(attack.knockback, attack.attackPos);

            cameraShake.trauma += attackScreenShake;

            StartCoroutine(damageInvulnerabilty(0.3f));
            }
        }

    }

    void Update()
    {
        health = ClampInt(health, maxHealth);
        energie = ClampInt(energie, maxEnergie);

        isAlive(player,1f);

        time += Time.deltaTime;
        EnergieFill(energieParSec);
    }

    public void EnergieFill(int EnergieParSec)
    {
        if (time > 1f)
        {
            energie += EnergieParSec;
            time = 0f;
        }
    }

    public void gainLife(int bonusHealth)
    {
        health += bonusHealth;
    }

    public int ClampInt(int value, int MaxValue)
    {
        return Mathf.Clamp(value, 0, MaxValue);
    }

    public void energieAttack(int energieCost)
    {
        energie -= energieCost;
    }

    new void TakeKnockBack(float knockbackPower, Transform attackSource)
    {
        // La direction de l'attaque
        Vector2 knockBackDirection = mySelf.position - attackSource.position;
        knockBackDirection.Normalize();

        // Subit le recul de l'attaque
        myBody.velocity = knockBackDirection * knockbackSensitivity * knockbackPower;

    }
}
