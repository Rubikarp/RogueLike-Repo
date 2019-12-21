using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [Header("Entités")]
    public Rigidbody2D body;
    public CapsuleCollider2D collid;
    public Transform checkPosFloor;
    public Transform checkPosWallLeft;
    public Transform checkPosWallRight;
    public Transform checkPosCeilling;
    public LayerMask TerrainLayerMask;

    [SerializeField]
    public CharacterInput input;

    #region Statuts

    [Header("Can")]
    public bool canMove = true;
    public bool canRun = true;
    public bool canJump = true;
    public bool canDash = true;
    public bool canAttack = true;

    [Header("Is")]
    public bool isLookingRight = true;
    public bool isSpeedLimit = true;

    public bool isMoving;
    public bool isWallJumping;
    public bool isRuning ;
    public bool isJumping ;
    public bool isDashing ;
    public bool isAttacking ;

    public bool isOnGround;
    public bool isOnWall;
    public bool isOnWallLeft;
    public bool isOnWallRight;
    public bool isOnCeilling;

    [Header("DistanceDetection")]
    [SerializeField] private float groundDetectDist     = 0.2f;
    [SerializeField] private float wallDetectDist       = 0.2f;
    [SerializeField] private float ceillingDetectDist   = 0.2f;


    #endregion

    private void Awake()
    {
        body = this.GetComponent<Rigidbody2D>();
        collid = this.GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        //detection sol & mur & plafond
        CheckingPos();


        if(input.lookingRight == 1)
        {
            isLookingRight = true;
        }
        else
        if (input.lookingRight == -1)
        {
            isLookingRight = false;
        }


        if (!isOnWallLeft && !isOnWallRight)
        {
            isOnWall = false;
        }
        else
        if (isOnWallLeft || isOnWallRight)
        {
            isOnWall = true;
        }

    }

    private void CheckingPos()
    {
        //Le calcul (x et y inversé je sais pas pourquoi)
        isOnGround    = Physics2D.OverlapBox(checkPosFloor.position,    new Vector2(collid.size.x - 0.2f, -groundDetectDist)  , 0, TerrainLayerMask);
        isOnWallLeft  = Physics2D.OverlapBox(checkPosWallLeft.position, new Vector2( -wallDetectDist, collid.size.y - 0.2f), 0, TerrainLayerMask);
        isOnWallRight = Physics2D.OverlapBox(checkPosWallRight.position,new Vector2( wallDetectDist, collid.size.y - 0.2f)    , 0, TerrainLayerMask);
        isOnCeilling  = Physics2D.OverlapBox(checkPosCeilling.position, new Vector2(collid.size.x - 0.2f, ceillingDetectDist) , 0, TerrainLayerMask);

        #region color gestion
        //Couleur
        Color castColorGround;
        Color castColorWallLeft;
        Color castColorWallRight;
        Color castColorCeilling;

        if (isOnGround == true)     { castColorGround = Color.green; }      else { castColorGround = Color.red;}
        if (isOnWallLeft == true)   { castColorWallLeft = Color.green; }    else { castColorWallLeft = Color.red; }
        if (isOnWallRight == true)  { castColorWallRight = Color.green; }   else { castColorWallRight = Color.red; }
        if (isOnCeilling == true)   { castColorCeilling = Color.green; }    else { castColorCeilling = Color.red; }
        #endregion

        //affichage sol                    Vecteur pour se placer          à droite                 en bas                     du collider          On trace vers la gauche                             avec la couleur attribué
        Debug.DrawRay(collid.bounds.center + new Vector3(collid.bounds.extents.x - 0.1f ,-(collid.bounds.extents.y + (groundDetectDist / 2)), 0) , Vector2.left * ((collid.bounds.extents.x - 0.1f) * 2), castColorGround);
        //affichage plafond
        Debug.DrawRay(collid.bounds.center + new Vector3(collid.bounds.extents.x - 0.1f ,  collid.bounds.extents.y + (groundDetectDist / 2) , 0) , Vector2.left * ((collid.bounds.extents.x - 0.1f) * 2), castColorCeilling);
        //affichage mur gauche
        Debug.DrawRay(collid.bounds.center + new Vector3(-(collid.bounds.extents.x + (wallDetectDist / 2)), -collid.bounds.extents.y - 0.1f, 0)  , Vector2.up * ((collid.bounds.extents.y - 0.1f) * 2)   , castColorWallLeft);
        //affichage mur droite
        Debug.DrawRay(collid.bounds.center + new Vector3(  collid.bounds.extents.x + (wallDetectDist / 2) , -collid.bounds.extents.y - 0.1f, 0)  , Vector2.up * ((collid.bounds.extents.y - 0.1f) * 2)   , castColorWallRight);

    }

}
