using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    [Header("à définir")]
    [HideInInspector] public Rigidbody2D body;
    public CapsuleCollider2D Collider2D;


    #region Input
    [Header("Les inputs")]

    //Direction du stick
    public Vector2 inputStickDirection;
    //Valeur du stick sur l'axe X et Y
    public float inputStickX = 0, inputStickY = 0;
    public float inputStickXabs = 0, inputStickYabs = 0;

    //touche activé ?
    public bool inputJump, inputDash, inputAttackLight, inputAttackHeavy;
    //touche viens d'être activée ?
    [HideInInspector] public bool inputJumpEnter, inputDashEnter, inputAttackLightEnter, inputAttackHeavyEnter;
    //touche viens d'être lachée ?
    [HideInInspector] public bool inputJumpExit, inputDashExit , inputAttackLightExit, inputAttackHeavyExit;

    #endregion

    #region Statuts
    [Header("Statuts")]

    public bool canMove = true;
    public bool canJump = true;
    public bool canDash = true;
    public bool canAttack = true;

    //regarde à droite
    public int lookingRight = 1;

    public bool isSpeedLimit = true;
    public float speedX = 0f;
    public float speedY = 0f;
    public float maxSpeedX = 800f;
    public float maxSpeedY = 800f;

    //En contacte avec le sol ?
    public bool isOnGround;
    public bool isOnWall;
    public Transform checkPosFloor;
    public Transform checkPosWall;
    [SerializeField] private float groundDetectionDistance = 0.2f;
    [SerializeField] private float wallDetectionDistance = 0.2f;
    public LayerMask TerrainLayerMask;
    #endregion 

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        #region PrendsLesInputs

        //Je prends les valeurs du stick
        inputStickX = Input.GetAxis("Horizontal");
        inputStickY = Input.GetAxis("Vertical");
        inputStickXabs = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        inputStickYabs = Mathf.Abs(Input.GetAxisRaw("Vertical"));

        inputStickDirection = new Vector2(inputStickX, inputStickY);

        //Je prends les buttons
        inputDash = Input.GetButton("Dash");
        inputDashEnter = Input.GetButtonDown("Dash");
        inputDashExit = Input.GetButtonUp("Dash");

        inputJump = Input.GetButton("Saut");
        inputJumpEnter = Input.GetButtonDown("Saut");
        inputJumpExit = Input.GetButtonUp("Saut");

        inputAttackLight = Input.GetButton("attackLight");
        inputAttackLightEnter = Input.GetButtonDown("attackLight");
        inputAttackLightExit = Input.GetButtonUp("attackLight");

        inputAttackHeavy = Input.GetButton("attackHeavy");
        inputAttackHeavyEnter = Input.GetButtonDown("attackHeavy");
        inputAttackHeavyExit = Input.GetButtonUp("attackHeavy");
        #endregion

        IslookingRight();

        CheckingPos();

    }
    private void FixedUpdate()
    {
        speedX = body.velocity.x;
        speedY = body.velocity.y;


        if (isSpeedLimit)
        {
            speedLimitationX(maxSpeedX);
        }

        speedLimitationY(maxSpeedY);
    }


    private void IslookingRight()
    {
        //regarder devant soi
        if (inputStickX > 0f)
        {
            lookingRight = 1;
            transform.eulerAngles = new Vector2(0, 0);
        }
        //se retourner
        else if (inputStickX < 0f)
        {
            lookingRight = -1;
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private void speedLimitationX(float maxSpeedX)
    {
        if(body.velocity.x >= maxSpeedX)
        {
            body.velocity = new Vector2(maxSpeedX, body.velocity.y);
        }

        if (body.velocity.x <= -maxSpeedX)
        {
            body.velocity = new Vector2(-maxSpeedX, body.velocity.y);
        }
    }
    private void speedLimitationY( float maxSpeedY)
    {
        if (body.velocity.y >= maxSpeedY)
        {
            body.velocity = new Vector2(body.velocity.x, maxSpeedY);
        }

        if (body.velocity.y <= -maxSpeedY)
        {
            body.velocity = new Vector2(body.velocity.x, -maxSpeedY);
        }
    }

    private void CheckingPos()
    {
        //Le calcul (x et y inversé je sais pas pourquoi)
        isOnGround = Physics2D.OverlapBox(checkPosFloor.position, new Vector2(Collider2D.size.x - 0.2f, groundDetectionDistance), 0, TerrainLayerMask);
        isOnWall = Physics2D.OverlapBox(checkPosWall.position, new Vector2(wallDetectionDistance,Collider2D.size.y - 0.2f), 0, TerrainLayerMask);

        //Couleur
        Color castColorGround;
        if (isOnGround == true)
        {
            castColorGround = Color.green;
        } else {
            castColorGround = Color.red;
        }
        Color castColorWall;
        if (isOnWall == true)
        {
            castColorWall = Color.green;
        } else {
            castColorWall = Color.red;
        }

        //affichage mur puis sol
        Debug.DrawRay(Collider2D.bounds.center + new Vector3(lookingRight * (Collider2D.bounds.extents.x + (wallDetectionDistance/2)), -Collider2D.bounds.extents.y-0.1f, 0), Vector2.up * ((Collider2D.bounds.extents.y - 0.1f) * 2), castColorWall);
        Debug.DrawRay(Collider2D.bounds.center + new Vector3(-Collider2D.bounds.extents.x - 0.1f,-(Collider2D.bounds.extents.y + (groundDetectionDistance / 2)), 0), Vector2.right * ((Collider2D.bounds.extents.x - 0.1f) * 2), castColorGround);

    }

    /*
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(Collider2D.bounds.center, Collider2D.bounds.size - new Vector3Int(8, 0, 0), 0f, Vector2.down, Collider2D.bounds.extents.y + groundDetectionDistance, TerrainLayerMask);
        Color castColor;
        if (raycastHit.collider != null)
        {
            castColor = Color.green;
        } else {
            castColor = Color.red;
        }

        //affiche la zone du raycast
        Debug.DrawRay(Collider2D.bounds.center + new Vector3(Collider2D.bounds.extents.x, 0), Vector2.down * (Collider2D.bounds.extents.y + groundDetectionDistance), castColor);
        Debug.DrawRay(Collider2D.bounds.center - new Vector3(Collider2D.bounds.extents.x, 0), Vector2.down * (Collider2D.bounds.extents.y + groundDetectionDistance), castColor);
        Debug.DrawRay(Collider2D.bounds.center - new Vector3(Collider2D.bounds.extents.x, Collider2D.bounds.extents.y + groundDetectionDistance), Vector2.right * (Collider2D.bounds.extents.x*2), castColor);


        //affiche ce que je touche (null ou qqc)
        Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;
    }
    private bool TouchingWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(Collider2D.bounds.center, Collider2D.bounds.size - new Vector3Int(0,8,0), 0f, lookingRight*(Vector2.right), Collider2D.bounds.extents.x + wallDetectionDistance, TerrainLayerMask);

        Color castColor;
        if (raycastHit.collider != null)
        {
            castColor = Color.green;
        } else {
            castColor = Color.red;
        }

        //affiche la zone du raycast
        Debug.DrawRay(Collider2D.bounds.center + new Vector3(0, Collider2D.bounds.extents.y ), (lookingRight *Vector2.right) * (2 * Collider2D.bounds.extents.x + wallDetectionDistance), castColor);
        Debug.DrawRay(Collider2D.bounds.center - new Vector3(0, Collider2D.bounds.extents.y ), (lookingRight * Vector2.right) * (2 * Collider2D.bounds.extents.x + wallDetectionDistance), castColor);
        Debug.DrawRay(Collider2D.bounds.center + new Vector3(lookingRight * 2 * Collider2D.bounds.extents.x + wallDetectionDistance, -Collider2D.bounds.extents.y), Vector2.up * (Collider2D.bounds.extents.y * 2), castColor);


        //affiche ce que je touche (null ou qqc)
        Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;
    }
    */
}