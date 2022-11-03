using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class playerController : MonoBehaviour


{

    // components
    public Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    

    //movement stuff
    public float speed;
    public float jumpForce;


    // groundcheck stuff
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius;

    //vairables 
    Coroutine jumpForceChange;
    public int maxLives = 5;
    private int _lives = 3;

    public int lives
    {
        get { return _lives; }
        set
        {
            //if (_lives > value)
            //we lost a life - we should respawn

            _lives = value;

            if (_lives > maxLives)
                _lives = maxLives;

            //if (_lives < 0)
            //gameover code goes here

            Debug.Log("Lives have been set to: " + _lives.ToString());
        }
    }


    public int GetLives()
    {
        return lives;
    }





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        

        if (speed <= 0)
        {
            speed = 6.0f;
            Debug.Log("Speed Not Set - Default To 6");

        }
        if (jumpForce <= 0)
        {
            jumpForce = 300;
            Debug.Log("Jump Force Not Set - Default To 300");

        }
        if (!groundCheck)
        {
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            Debug.Log("Ground Check Not Set - Finding It Manually");
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            Debug.Log("Ground Check Radius Not Set - Default To 0.2");
        }



    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (curPlayingClip.Length > 0)
        {
            if (Input.GetButtonDown("Fire1") && curPlayingClip[0].clip.name != "Fire")
            {
                anim.SetTrigger("Fire");
                
            }
            else if (curPlayingClip[0].clip.name == "Fire")
                rb.velocity = Vector2.zero;
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (!isGrounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("JumpAttack");
        }


        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);





        //check for flipped and some sort of algorithm to keep your sprite flipped properly.

        if (hInput != 0)
            sr.flipX = (hInput < 0);

        if (isGrounded)
            rb.gravityScale = 1;
    }

    public void IncreaseGravity()
    {
        rb.gravityScale = 5;
    }


} 
