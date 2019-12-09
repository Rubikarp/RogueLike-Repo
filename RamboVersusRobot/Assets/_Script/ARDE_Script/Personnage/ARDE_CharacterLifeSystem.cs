using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_CharacterLifeSystem : ARDE_LifeSystem
{
    public GameObject player = default;

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
        myBody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        health = ClampInt(health, maxHealth);
        energie = ClampInt(energie, maxEnergie);

        isAlive(player);

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

}
