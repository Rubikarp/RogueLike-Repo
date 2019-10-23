using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackLourd1 : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackOri;
    public float attackRange;
    public LayerMask whatIsEnnemies;
    public int damage;
    private float moveInput;

    public float distance;
    public Transform prefab;

    public float offset;
    public Transform attackLourd1Pos;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        moveInput = Input.GetAxis("Horizontal");




        if (timeBtwAttack <= 0)
        {
            //Maintenant tu peux attaquer !

            if (Input.GetKey(KeyCode.JoystickButton3))
            {





                

                Debug.Log("Attack");

                if (moveInput == -1)
                {
                    Instantiate(prefab, new Vector2(attackLourd1Pos.position.x, attackLourd1Pos.position.y), Quaternion.identity);

                    Collider2D[] ennemiesToDamage = Physics2D.OverlapCircleAll(new Vector2(attackLourd1Pos.position.x, attackLourd1Pos.position.y), attackRange, whatIsEnnemies);
                    for (int i = 0; i < ennemiesToDamage.Length; i++)
                    {

                        ennemiesToDamage[i].GetComponent<EnemyBase>().TakeDamage(damage);
                        Debug.Log("Damage : Lourd1");

                    }

                }
                else if (moveInput == 1)
                {

                    Instantiate(prefab, new Vector2(attackLourd1Pos.position.x - 2, attackLourd1Pos.position.y), Quaternion.identity);

                    Collider2D[] ennemiesToDamage = Physics2D.OverlapCircleAll(new Vector2(attackLourd1Pos.position.x - 2, attackLourd1Pos.position.y), attackRange, whatIsEnnemies);
                    for (int i = 0; i < ennemiesToDamage.Length; i++)
                    {

                        ennemiesToDamage[i].GetComponent<EnemyBase>().TakeDamage(damage);
                        Debug.Log("Damage : Lourd1");

                    }

                }





            }

            timeBtwAttack = startTimeBtwAttack;

        }
        else
        {

            timeBtwAttack -= Time.deltaTime;

        }
        
    }
    
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(attackLourd1Pos.position.x, attackLourd1Pos.position.y), attackRange);

    }
    
}