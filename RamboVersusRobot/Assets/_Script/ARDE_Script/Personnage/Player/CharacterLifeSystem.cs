using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLifeSystem : MonoBehaviour
{

    public GameObject character = null;
    public Animator anim = null;
    public SpriteRenderer spriteRender = null;

    public int health = 5;
    private int maxHealth = 7;

    public int energie = 5;
    private int maxEnergie = 10;


    private void Start()
    {


    }

    void Update()
    {
        gainLife(health);
        takeDammage(health);
        isAlive(health);
        
    }

    private void isAlive(int health)
    {
        //est-il en vie ?
        if (health <= 0)
        {
            /* Animation de mort
            animator.play();
            WaitForSeconds(tpDeL'animation);
            */

            Destroy(this.gameObject);


        }
    }

    private void takeDammage(int health)
    {

    }

    private void gainLife(int health)
    {

    }

}

