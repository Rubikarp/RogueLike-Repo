using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public Rigidbody2D srb;
    private float Cooldown = 2;

    public LayerMask Player;
    public Vector2 direction;

    public float speed;

    private float dirx;
    private float diry;

    private void Start()
    {

        //On cherche a savoir qui le projectile vise ici ce sera probablement le joueur
        player = GameObject.FindGameObjectWithTag("Player").transform;

        dirx = player.position.x;
        diry = player.position.y;

        direction = new Vector2((dirx - transform.position.x), (diry - transform.position.y)) * speed;
    }

    private void FixedUpdate()
    {

        //On fait bouger le projectile vers la position du joueur sauvegardé au lancement
        srb.velocity = direction;


        Cooldown -= Time.deltaTime;

        if (Cooldown <= 0)
        {

            Destroy(gameObject);

        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //Quand le projectile entre en contact et si c'est le joueur, alors le joueur prend des dégâts et du knockback
        

        if (other.CompareTag("Player"))
        {

            other.GetComponent<PlayerLifeSystem>().TakeDamage(1);
            other.GetComponent<PlayerLifeSystem>().TakeKnockBack(10, transform);
            Destroy(gameObject);

        }

        

    }




}
