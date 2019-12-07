using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_EnnemisBehavior : MonoBehaviour
{
    [Header("Auto")]
    protected Transform mySelf = null;
    protected Rigidbody2D myBody = null;
    protected CircleCollider2D myCollider = null;
    public Transform player = null;

    [Header("à def")]
    public LayerMask TerrainLayerMask;

    [Header("tweaking")]
    public float speed;

    [Space(10)]
    public float detectionRange;
    public float ToNearDistance;
    public float ToFarDistance;

    [Header("inside")]
    //Private Values
    [SerializeField] protected Vector2 playerDirection;
    [SerializeField] protected float playerDistance;

    [Space(10)]
    [SerializeField] protected bool playerDetecting;
    [SerializeField] protected bool playerToNear;
    [SerializeField] protected bool playerToFar;

    [Space(10)]
    [SerializeField] protected bool IsAttacking;


    private void Start()
    {
        mySelf = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myCollider = this.GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        #region Variables

        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - transform.position);
        //calcul la distance entre le GameObject et le joueur
        playerDistance = Vector2.Distance(transform.position, player.position);

        #endregion

        PlayerRelativeToTheEnnemy(playerDistance, detectionRange, ToNearDistance, ToFarDistance);
    }

    protected void PlayerRelativeToTheEnnemy(float playerDistance, float detectionRange, float ToNearDistance, float ToFarDistance)
    {
        //Est ce que je detecte le joueur ?
        if (detectionRange > playerDistance)
        {
            playerDetecting = true;

            //je m'approche le joueur s'il est trop loin
            if (playerDistance > ToNearDistance)
            {
                playerToNear = true;
            }
            //je m'eloigne du joueur s'il est trop proche
            else if (playerDistance < ToFarDistance)
            {
                playerToNear = false;
                playerToFar = true;
            }
            else
            {
                playerToNear = false;
                playerToFar = false;
            }
        }
        else
        {
            playerDetecting = false;
            playerToNear = false;
            playerToFar = false;
        }
    }

}
