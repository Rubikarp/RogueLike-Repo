using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header ("information")]
    [SerializeField] private float lifeTime = 0.3f;
    public float damage = 1f;

    void Update()
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
            SelfDestruction();
        } 
    }

    void SelfDestruction()
    {
        Destroy(gameObject);
    }

}
