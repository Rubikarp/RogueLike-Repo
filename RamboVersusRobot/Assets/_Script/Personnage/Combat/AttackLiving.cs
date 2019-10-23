using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLiving : MonoBehaviour
{

    [SerializeField] private float lifeTime = 0.3f;

    //[SerializeField] private float damage = 1f;


    void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                autoDestruction();
            }
        }
    }

    void autoDestruction()
    {
        Destroy(this.gameObject);
    }
}
