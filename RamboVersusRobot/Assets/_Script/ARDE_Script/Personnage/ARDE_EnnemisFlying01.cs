using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDE_EnnemisFlying01 : ARDE_EnnemisBehavior
{
    [Header("Flying Base")]
    public float flyForce = 1f;
    public float detectDist = 5f;

    [Header("Flying Base inside")]
    [SerializeField] float rotZ = 0f;

    void Update()
    {
        FacePlayer();
        DontCrash();
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
}
