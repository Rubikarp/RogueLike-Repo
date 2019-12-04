using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    
    //Variables pour la Vitesse
    [SerializeField] public float speed;

    //Récupérer le rigidbody du robot et le transform du joueur
    [SerializeField] public Rigidbody2D srb;
    [SerializeField] public Transform player;

    //Détécter quand le robot est endormi et sa zone de détéction
    [SerializeField] protected bool IsSleeping = true;
    [SerializeField] protected bool IsAttacking = false;
    [SerializeField] protected bool IsNothing;

    public int detectionRange;
    public int AttackRange;
    public float AttackCooldown = 5;
    public int AttackDamage;
    public int Knockback;

    public float atkposx;
    public float atkposy;

    //Deux variables modifiables a partir d'un script enfant pour modifier la position de l'attaque en fonction du sprite du robot
    protected float atkxoffset;
    protected float atkyoffset;

    [SerializeField] protected float Cooldown;
    public LayerMask Player;
    protected float distToPlayer;
    protected Vector2 playerDirection;





    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        Cooldown = AttackCooldown;
    }

    private void Update()
    {
                   
    }

    protected virtual void FixedUpdate()
    {

        distToPlayer = Vector2.Distance(transform.position, player.position);

        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - transform.position);






        //Détermine si le robot est réveillé ou non ou s'il est a porté d'attaque.

        if (distToPlayer < AttackRange + 2 && Cooldown < 0 && IsNothing == false)
        {

            IsAttacking = true;
            Attack();
            Cooldown = AttackCooldown;
            IsAttacking = false;

        }
        else if (distToPlayer > detectionRange && IsNothing == false)
        {

            IsSleeping = true;
            SleepingBehaviour();

        }
        else if (distToPlayer < detectionRange && IsNothing == false && IsAttacking == false)
        {

            IsSleeping = false;
            AwakenBehaviour();

        } 

        //Détermine ce que fait le robot quand il est réveillé ou endormi ou quand il attaque.
        Cooldown -= Time.deltaTime;

    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////Fonctions/////////////////////////////////////////// 
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    

    //Fonction pour détruire le robot.
    protected void Destruction()
    {

        GameObject.Destroy(gameObject);

    }

    //Fonction de Base pour faire bouger le robot d'une position vers une autre en ligne droite
    protected void MoveToPosition(Transform destination, float speed)
    {

        srb.velocity = new Vector2((destination.position.x - transform.position.x) * speed, (destination.position.y - transform.position.y) * speed);

    }

    protected void MoveAwayFromPosition(Transform destination, float speed)
    {

        srb.velocity = new Vector2((destination.position.x + transform.position.x) * speed, (destination.position.y + +transform.position.y) * speed);

    }

    //Version de la fonction pour uniquement bouger la position horizontalement
    protected void HoriMoveToPosition(Transform destination, float speed)
    {

        srb.velocity = new Vector2((destination.position.x - transform.position.x) * speed, 0);

    }

    //Version de la fonction pour uniquement bouger la position verticalement.
    protected void VertiMoveToPosition(Transform destination, float speed)
    {

        srb.velocity = new Vector2(0, (destination.position.y - transform.position.y) * speed);

    }

    protected void FacePlayer()
    {
        //calcul l'angle pour faire face au joueur
        float rotZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        //oriente l'object pour faire face au joueur
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    //Les Fonctions de behaviour du robot afin de savoir comme il agit quand il est endormi ou réveillé et ce qu'il se passe quand il attaque.
    //Les fonctions sont vides pour l'instant, le but est de les modifier dans le script de chaque type d'ennemi mais si on veut un joueur ajouter un truc
    // a toutes leurs attaques, on pourra.
    protected virtual void SleepingBehaviour()
    {

       
        

    }

    protected virtual void AwakenBehaviour()
    {

       
 

    }

    protected virtual void Attack()
    {

       


    }

    
}
