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
        SelfDestructionIn(lifeTime);
    }

    private void SelfDestructionIn(float lifeTime)
    {
        //Si je ne suis trop vieux
        if (lifeTime > 0)
        {
            //je vis
            lifeTime -= Time.deltaTime;
        }
        else //sinon
        {
            //je meurt
            Destroy(gameObject);
        }
    }

}
