using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_AttackSystem : MonoBehaviour
{
    [HideInInspector] public Transform attackPos = default;
    private CharacterState state = default;

    public GameObject attack;

    public int damage = 1;
    public float knockback = 40f;

    public bool haveLifeTime = true;
    public bool DestroyYesDesactivateNo = true;
    public float startLifeTime = 0.3f;
    public float lifeTime = 0.3f;

    private void Start()
    {
        attackPos = this.transform;
        state = GetComponentInParent<CharacterState>();

    }

    void Update()
    {
        if (haveLifeTime)
        {
            if (lifeTime < 0)
            {
                lifeTime = startLifeTime;
                state.canAttack = true;

                //je meurt
                if (DestroyYesDesactivateNo)
                {
                    Destroy(attack);
                }
                else
                {
                    attack.SetActive(false);
                }

            }
            else
            {
                lifeTime -= Time.deltaTime;
            }

        }
    }
    
    
}
