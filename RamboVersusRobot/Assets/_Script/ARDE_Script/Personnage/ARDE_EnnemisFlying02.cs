using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_EnnemisFlying02 : ARDE_EnnemisBehavior
{
    [Header("Flying Base")]
    public float flyForce = 1f;
    public float detectDist = 5f;

    public GameObject attackZone;
    public float attackDuration = 1f;
    public float dashSpeed = 20f;
    public float attackCoolDown = 0.5f;

    [Header("Flying Base inside")]
    [SerializeField] float rotZ = 0f;
    [SerializeField] bool haveAttack = false;

    void Update()
    {
        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - transform.position);
        //calcul la distance entre le GameObject et le joueur
        playerDistance = Vector2.Distance(transform.position, player.position);

        PlayerRelativeToTheEnnemy(playerDistance, detectionRange, ToNearDistance, ToFarDistance);

        FacePlayer();
        DontCrash();
        RushPlayer();

    }

    protected void FacePlayer()
    {
        //calcul l'angle pour faire face au joueur
        rotZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        //oriente l'object pour faire face au joueur
        mySelf.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    protected void DontCrash()
    {
        RaycastHit2D ground = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, detectDist, TerrainLayerMask);
        RaycastHit2D wallLeft = Physics2D.Raycast(myCollider.bounds.center, Vector2.left, detectDist / 2, TerrainLayerMask);
        RaycastHit2D wallRight = Physics2D.Raycast(myCollider.bounds.center, Vector2.right, detectDist / 2, TerrainLayerMask);
        RaycastHit2D ceilling = Physics2D.Raycast(myCollider.bounds.center, Vector2.up, detectDist, TerrainLayerMask);

        if (ground) { myBody.velocity += Vector2.Lerp(Vector2.zero, new Vector2(0, flyForce), Time.deltaTime); }
        if (wallLeft) { myBody.velocity += Vector2.Lerp(Vector2.zero, new Vector2(flyForce, 0), Time.deltaTime) ; }
        if (wallRight) { myBody.velocity += Vector2.Lerp(Vector2.zero, new Vector2(-flyForce, 0), Time.deltaTime); }
        if (ceilling) { myBody.velocity += Vector2.Lerp(Vector2.zero, new Vector2(0, -flyForce), Time.deltaTime); }

    }

    protected void RushPlayer()
    {
        if (playerDetecting)
        {
            if (playerToNear)
            {
                if (!haveAttack)
                {
                    StartCoroutine(Attack(attackCoolDown, playerDirection));
                }
                myBody.velocity += Vector2.Lerp(Vector2.zero, -playerDirection.normalized * speed , Time.deltaTime);
            }
            else
            if (playerToFar)
            {
                myBody.velocity += Vector2.Lerp(Vector2.zero, playerDirection.normalized * speed, Time.deltaTime);
            }
            else
            {
                myBody.velocity += -myBody.velocity.normalized * 5f * Time.deltaTime;
            }
        }
        else
        {
            myBody.velocity = Vector2.zero;
        }
    }

    IEnumerator Attack(float CoolDown, Vector2 playerDirection)
    {
        float time = 0f;

        attackZone.SetActive(true);
        haveAttack = true;


        while (attackDuration > time) 
        {
            time += Time.deltaTime;
            myBody.velocity = playerDirection.normalized * dashSpeed;
            yield return 0;
        }

        attackZone.SetActive(false);
        myBody.velocity = Vector2.zero;


        yield return new WaitForSeconds(CoolDown);
        haveAttack = false;

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, playerDirection.normalized * detectionRange, Color.blue);
        Debug.DrawRay(transform.position, playerDirection.normalized * ToFarDistance, Color.red);
        Debug.DrawRay(transform.position, playerDirection.normalized * ToNearDistance, Color.green);
    }
}
