using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    //Variables pour la Vitesse
    [SerializeField] public int speed;

    //Récupérer le rigidbody du robot et le transform du joueur
    [SerializeField] public Rigidbody2D srb;
    [SerializeField] public Transform player;

    //Détécter quand le robot est endormi et sa zone de détéction
    [SerializeField] private bool IsSleeping = true;
    [SerializeField] public int detectionRange;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {

        //Détermine si le robot est réveillé ou non.
        if (Vector2.Distance(transform.position, player.position) > detectionRange)
        {

            IsSleeping = true;

        } else if (Vector2.Distance(transform.position, player.position) < detectionRange)
        {

            IsSleeping = false;

        }
                
    }

    private void FixedUpdate()
    {
        
        //Détermine ce que fait le robot quand il est réveillé ou endormi.
        if (IsSleeping == false)
        {
            //Comportement réveillé
            AwakenBehaviour();
            

        } else if (IsSleeping == true)
        {
            //Comportement Endormi
            SleepingBehaviour();
            
        }


    }


    //Fonction pour détruire le robot.
    protected void Destruction()
    {

        GameObject.Destroy(gameObject);

    }

    //Fonction de Base pour faire bouger le robot d'une position vers une autre en ligne droite
    protected void MoveToPosition(Transform destination, int speed)
    {

        srb.velocity = new Vector2((destination.position.x - transform.position.x) / speed, (destination.position.y - transform.position.y) / speed);

    }

    //Version de la fonction pour uniquement bouger la position horizontalement
    protected void HoriMoveToPosition(Transform destination, int speed)
    {

        srb.velocity = new Vector2((destination.position.x - transform.position.x) / speed, 0);

    }

    //Version de la fonction pour uniquement bouger la position verticalement.
    protected void VertiMoveToPosition(Transform destination, int speed)
    {

        srb.velocity = new Vector2(0, (destination.position.y - transform.position.y) / speed);

    }

    protected virtual void SleepingBehaviour()
    {

        //Debug.Log("Sleeping");
        

    }

    protected virtual void AwakenBehaviour()
    {

        //Debug.Log("Awaken");
 

    }

    protected virtual void Attack()
    {

        //Debug.Log("Attack");


    }
}
