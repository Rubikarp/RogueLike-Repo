using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_EnnemisGround01Attack : ARDE_AttackSystem
{
    public float attackOffSet = 1f;

    [SerializeField] Transform player = null;
    [SerializeField] protected Vector3 playerDirectionNorm;
    [SerializeField] float rotZ;

    void Start()
    {
        attackPos = this.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerDirectionNorm = (player.position - transform.position).normalized;

        //Attaque vers le jouer avec un offset
        attackPos.position = attackPos.position + playerDirectionNorm * attackOffSet;

        //calcul l'angle pour faire face au joueur
        rotZ = Mathf.Atan2(playerDirectionNorm.y, playerDirectionNorm.x) * Mathf.Rad2Deg;
        //oriente l'object pour faire face au joueur
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
