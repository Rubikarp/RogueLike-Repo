using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpforce;
    private float moveInput;

    private Rigidbody2D rb;
    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;
    
    
    // Start is called before the first frame update
    void Start()
    {

        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isGrounded == true)
        {

            extraJumps = extraJumpsValue;

        }


        if (Input.GetKeyDown(KeyCode.JoystickButton0) && extraJumps > 0)
        {

            rb.velocity = Vector2.up * jumpforce;
            extraJumps--;

        } /*else if (Input.GetKeyDown(KeyCode.JoystickButton0) && extraJumps == 0 && isGrounded == true)
        {

            rb.velocity = Vector2.up * jumpforce;

        }*/



    }

    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);



        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0)
        {

            
            Debug.Log("Right");

        } else if (facingRight == true && moveInput < 0)
        {

            
            Debug.Log("Left");

        }


    }

    void Flip()
    {

        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }


}
