using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_EnnemisGround01 : ARDE_EnnemisBehavior
{
    [Header("Ground Base")]
    public GameObject attack;
    public Transform attackContainer;
    public float attackCoolDown = 0.3f;
    public bool haveAttack = false;

    
    void Update()
    {
        TrackPlayer();

    }

    protected void TrackPlayer()
    {
        if (playerDetecting)
        {
            if (playerToNear)
            {
                StartCoroutine(Attack(3));
                myBody.velocity = new Vector2(-playerDirection.x, 0) * speed;
            }
            else
            if (playerToFar)
            {
                myBody.velocity = new Vector2(-playerDirection.x, 0) * speed;
            }
        }
    }

    IEnumerator Attack(float CoolDown)
    {
        Instantiate(attack, mySelf.position, mySelf.rotation, attackContainer);
        haveAttack = true;

        yield return new WaitForSeconds(CoolDown);
        haveAttack = false;

    }
}
