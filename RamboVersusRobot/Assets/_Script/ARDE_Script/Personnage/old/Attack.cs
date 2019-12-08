using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header ("information")]
    public float lifeTime = 0.3f;
    public int damage = 1;
    public float knockback = 40f;

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            //je meurt
            Destroy(this.gameObject);
        }
    }

}
