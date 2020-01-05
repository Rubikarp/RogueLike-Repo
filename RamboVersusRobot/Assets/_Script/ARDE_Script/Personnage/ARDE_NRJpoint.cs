using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ARDE_NRJpoint : MonoBehaviour
{
    [Header("Auto")]
    [HideInInspector]
    public Transform mySelf = null;
    protected Rigidbody2D myBody = null;
    protected CircleCollider2D myCollider = null;
    protected Transform player = null;
    protected ARDE_SoundManager soundManager = default;

    [Header("tweaking")]
    public float speed = 10;
    public float spread = 10;
    public float activationTime = 0.3f;
    public GameObject particule;

    bool activation = false;
    float time = 0f;

    [SerializeField] public Vector2 playerDirection;
    [SerializeField] public float playerDistance;

    void Start()
    {
        mySelf = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myCollider = this.GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        myBody.velocity = new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread));
    }

    void Update()
    {
        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - mySelf.position).normalized;

        //FacePlayer();

        if(time > activationTime)
        {
            FollowPlayer();
        }
        else
        {
            time += Time.deltaTime;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Quand le projectile entre en contact avec joueur, alors le joueur prend gagne  de l'energie
        if (other.CompareTag("Player"))
        {
            Destroy(particule);
        }

    }

    void FacePlayer()
    {
        //calcul l'angle pour faire face au joueur
        float rotZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        //oriente l'object pour faire face au joueur
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    void FollowPlayer()
    {
        myBody.velocity = playerDirection.normalized * speed;
    }
}
