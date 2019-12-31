using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_EnnemisFlying01 : ARDE_EnnemisBehavior
{
    [Header("Flying Base")]
    public float flyForce = 1f;
    public float detectDist = 5f;
    public float startBulletDistance = 2f;

    public GameObject bullet;
    public Transform bulletContainer;
    public float shootCoolDown = 1f;

    [Header("Flying Base inside")]
    [SerializeField] float rotZ = 0f;
    [SerializeField] bool haveShoot = false;

    void Update()
    {
        //defini à chaque frame dans quel direction est le joueur
        playerDirection = (player.position - transform.position);
        //calcul la distance entre le GameObject et le joueur
        playerDistance = Vector2.Distance(transform.position, player.position);

        PlayerRelativeToTheEnnemy(playerDistance, detectionRange, ToNearDistance, ToFarDistance);

        FacePlayer();
        DontCrash();
        TrackPlayer();

        if (!haveShoot && playerDetecting)
        {
            StartCoroutine(Attack(shootCoolDown));
        }

    }

    protected void FacePlayer()
    {
        //calcul l'angle pour faire face au joueur
        rotZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        //oriente l'object pour faire face au joueur
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    protected void DontCrash()
    {
        RaycastHit2D ground = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, detectDist, TerrainLayerMask);
        RaycastHit2D wallLeft = Physics2D.Raycast(myCollider.bounds.center, Vector2.left, detectDist / 2, TerrainLayerMask);
        RaycastHit2D wallRight = Physics2D.Raycast(myCollider.bounds.center, Vector2.right, detectDist / 2, TerrainLayerMask);
        RaycastHit2D ceilling = Physics2D.Raycast(myCollider.bounds.center, Vector2.up, detectDist, TerrainLayerMask);

        if (ground)    { myBody.velocity += Vector2.Lerp(Vector2.zero, new Vector2( 0, flyForce), Time.deltaTime) * speed; }
        if (wallLeft)  { myBody.velocity += Vector2.Lerp(Vector2.zero, new Vector2( flyForce, 0), Time.deltaTime) * speed; }
        if (wallRight) { myBody.velocity += Vector2.Lerp(Vector2.zero, new Vector2(-flyForce, 0), Time.deltaTime) * speed; }
        if (ceilling)  { myBody.velocity += Vector2.Lerp(Vector2.zero, new Vector2(0, -flyForce), Time.deltaTime) * speed; }

    }

    protected void TrackPlayer()
    {
        if (playerDetecting)
        {
            if (playerToNear)
            {
                myBody.velocity += Vector2.Lerp(Vector2.zero, -playerDirection.normalized * speed, Time.deltaTime);
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
            myBody.velocity += -myBody.velocity.normalized * 15f * Time.deltaTime;
        }

        myBody.velocity += -myBody.velocity.normalized * 5f * Time.deltaTime;

    }

    IEnumerator Attack(float CoolDown)
    {
        Instantiate(bullet, mySelf.position + playerDirection.normalized * startBulletDistance, mySelf.rotation, bulletContainer);
        soundManager.Play("RobotShoot");

        haveShoot = true;

        yield return new WaitForSeconds(CoolDown);

        haveShoot = false;

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, playerDirection.normalized * detectionRange, Color.blue);
        Debug.DrawRay(transform.position, playerDirection.normalized * ToFarDistance, Color.red);
        Debug.DrawRay(transform.position, playerDirection.normalized * ToNearDistance, Color.green);
    }
}
