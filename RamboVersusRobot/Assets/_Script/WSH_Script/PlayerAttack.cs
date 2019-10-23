using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackOri;
    public float attackRange;
    public LayerMask whatIsEnnemies;
    public int damage;


    public float distance;
    public Transform prefab;

    public float offset;
    public Transform attackPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //stick en angle
        float rotZ = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
        attackOri.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        attackPos.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);


        if (timeBtwAttack <= 0)
        {
            //Maintenant tu peux attaquer !

            if (Input.GetKey(KeyCode.JoystickButton2))
            {


                


                Instantiate(prefab, attackOri);

                Debug.Log("Attack");

                Collider2D[] ennemiesToDamage = Physics2D.OverlapCircleAll(new Vector2(attackPos.position.x, attackPos.position.y), attackRange, whatIsEnnemies);
                for (int i = 0; i < ennemiesToDamage.Length; i++)
                {

                    ennemiesToDamage[i].GetComponent<EnemyBase>().TakeDamage(damage);
                    Debug.Log("Damage");

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
        Gizmos.DrawWireSphere(new Vector2(attackPos.position.x, attackPos.position.y), attackRange);
      
    }

}
