using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public int health;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

        if (health <= 0)
        {

            GameObject.Destroy(gameObject);

        }


    }

    public void TakeDamage(int damage)
    {

        health -= damage;
        Debug.Log("damage TAKEN !");

    }
}
