using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public SpriteRenderer sprite = null;

    #region Input

    [Header("Inputs")]
    [HeaderAttribute("Stick")]

    //Direction du stick
    public Vector2 stickDirection;
    public Vector2 stickDirectionBrut;

    //Valeur du stick sur l'axe X et Y
    public float stickX = 0, stickY = 0;
    public float stickXabs = 0, stickYabs = 0;

    [HeaderAttribute("Jump")]
    public bool jumpEnter;
    public bool jump;
    public bool jumpExit;

    [HeaderAttribute("Dash")]
    public bool dashEnter;
    public bool dash;
    public bool dashExit;

    [HeaderAttribute("AttackLight")]
    public bool attackLightEnter;
    public bool attackLight;
    public bool attackLightExit;

    [HeaderAttribute("attackHeavy")]
    public bool attackHeavyEnter;
    public bool attackHeavy;
    public bool attackHeavyExit;

    [HeaderAttribute("Grab")]
    public bool grabEnter;
    public bool grab;
    public bool grabExit;

    #endregion Input

    [Header("Herited")]
    public int lookingRight = 1;

    private void Update()
    {
        #region PrendsLesInputs

        //Je prends les valeurs du stick
        stickX = Input.GetAxis("Horizontal");
        stickY = Input.GetAxis("Vertical");
        stickXabs = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        stickYabs = Mathf.Abs(Input.GetAxisRaw("Vertical"));

        stickDirection = new Vector2(stickX, stickY).normalized;
        stickDirectionBrut = new Vector2(stickX, stickY);

        //Je prends les buttons
        dash = Input.GetButton("Dash");
        dashEnter = Input.GetButtonDown("Dash");
        dashExit = Input.GetButtonUp("Dash");

        jump = Input.GetButton("Saut");
        jumpEnter = Input.GetButtonDown("Saut");
        jumpExit = Input.GetButtonUp("Saut");

        attackLight = Input.GetButton("attackLight");
        attackLightEnter = Input.GetButtonDown("attackLight");
        attackLightExit = Input.GetButtonUp("attackLight");

        attackHeavy = Input.GetButton("attackHeavy");
        attackHeavyEnter = Input.GetButtonDown("attackHeavy");
        attackHeavyExit = Input.GetButtonUp("attackHeavy");

        grab = Input.GetButton("Grab");
        grabEnter = Input.GetButtonDown("Grab");
        grabExit = Input.GetButtonUp("Grab");

        #endregion PrendsLesInputs

        IslookingRight();
    }

    private void IslookingRight()
    {
        //regarder devant soi
        if (stickX > 0f)
        {
            lookingRight = 1;
            sprite.flipX = false;
        }
        //se retourner
        else if (stickX < 0f)
        {
            lookingRight = -1;
            sprite.flipX = true;
        }
    }
}