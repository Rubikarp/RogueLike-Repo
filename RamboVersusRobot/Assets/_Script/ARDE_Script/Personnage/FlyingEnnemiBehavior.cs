using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnnemiBehavior : MonoBehaviour
{
    [Header("à def")]
    public Transform player = null;
    public Transform me = null;
    public CircleCollider2D myCollider = null;
    public Rigidbody2D body = null;
    public LayerMask TerrainLayerMask;

    [Header("tweaking")]
    public float speed;
    public float detectionRange, stoppingDistance, retreatDistance;

    //Private Values
    [SerializeField] private Vector2 playerDirection;
    [SerializeField] private float playerDistance;

    [SerializeField] private float flyForce = 1f;
    [SerializeField] private float detectDist = 5f;


    void Update()
    {
        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - transform.position);
        //calcul la distance entre le GameObject et le joueur
        playerDistance = Vector2.Distance(transform.position, player.position);

        FacePlayer();
    }

    private void FixedUpdate()
    {

        OnNearPlayer(playerDistance, detectionRange, stoppingDistance, retreatDistance);

        DontCrash();
    }

    void FacePlayer()
    {
        //calcul l'angle pour faire face au joueur
        float rotZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        //oriente l'object pour faire face au joueur
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    void OnNearPlayer(float playerDist, float detectRange, float stopDist, float retratDist)
    {
        //Est ce que je detecte le joueur ?
        if (detectionRange > playerDistance)
        {
            //je m'approche le joueur s'il est trop loin
            if (playerDistance > stoppingDistance)
            {
                body.velocity += playerDirection / (100/speed);
            }
            //je m'eloigne du joueur s'il est trop proche
            else if (playerDistance < retreatDistance)
            {
                body.velocity += -playerDirection / (50/speed);
            }
            else
            {
                body.velocity -= 0.2f * body.velocity;
            }
        }
    }

    void DontCrash()
    {
        RaycastHit2D ground    = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, detectDist    , TerrainLayerMask);
        RaycastHit2D wallLeft  = Physics2D.Raycast(myCollider.bounds.center, Vector2.left, detectDist / 2, TerrainLayerMask);
        RaycastHit2D wallRight = Physics2D.Raycast(myCollider.bounds.center, Vector2.right, detectDist / 2, TerrainLayerMask);
        RaycastHit2D ceilling  = Physics2D.Raycast(myCollider.bounds.center, Vector2.up, detectDist    , TerrainLayerMask);

        if (ground)    { body.velocity += new Vector2 (0, flyForce)  ; }
        if (wallLeft)  { body.velocity += new Vector2 (flyForce, 0)  ; }
        if (wallRight) { body.velocity += new Vector2 (-flyForce, 0) ; }
        if (ceilling)  { body.velocity += new Vector2 (0, -flyForce) ; }

    }

}
