using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    public Transform player;
    public Rigidbody2D rb;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;

    public float detectionRange;
    public LayerMask whatIsPhysic;




    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        //rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(player.position) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);


        if (timeBtwShots <= 0)
        {

            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;

        }
        else
        {

            timeBtwShots -= Time.deltaTime;

        }



    }

    private void FixedUpdate()
    {

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {

            //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            //rb.AddForce(new Vector2(player.position.x, player.position.y) * speed);
            //rb.velocity = new Vector2(player.position.x, player.position.y);
            //rb.MovePosition(player.position / speed);
            rb.velocity = new Vector2((player.position.x - transform.position.x) / speed, (player.position.y - transform.position.y) / speed);
            Debug.Log("ToPlayer");

        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {

            //transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            //rb.AddForce(new Vector2(0, 1) * speed);
            rb.velocity = new Vector2((transform.position.x - player.position.x) / speed, (transform.position.y - player.position.y) / speed);
            //rb.MovePosition(player.position);
            Debug.Log("Retreat");

        }
        /*else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {

            //transform.position = this.transform.position;
            //rb.velocity = new Vector2(transform.position.x, transform.position.y);
            Debug.Log("Stop");

        }*/




    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(1, 1));

    }
}
