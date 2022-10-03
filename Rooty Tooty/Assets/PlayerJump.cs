using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    public float jumpHeight = 5f;
    public bool isJumping = false;
    public Transform ceilingCheck;              //make sure the character can't jump past a ceiling
    public Transform groundCheck;               //make sure the character doesn't fall through the ground
    public LayerMask groundObjects;             //layer to assign the platforms and ground to ground so that we can check when the player is landed.
    private float checkRadius;
    public int MAXJUMPS = 2;                    //Double Jumping or more
    public int jumpCounter;                    //Current amount of jumps
    private bool isGrounded;
    private float jumpTimeCounter;
    public float TOTALJUMPTIME = 0.35f;
    private bool jumpHold;

    public Animator animator;                   //Link the animator to this script so that it will change with the correct input
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        jumpCounter = 0;
        animator.SetInteger("JumpCount", jumpCounter);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("yVelocity", rb.velocity.y);

        if (Input.GetButtonDown("Jump") && jumpCounter < MAXJUMPS)
        {
            animator.SetBool("Jump", true);
            isJumping = true;
            jumpTimeCounter = TOTALJUMPTIME;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpHold = false;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);

        if (isGrounded)
        {
            jumpCounter = 0;
            animator.SetInteger("JumpCount", jumpCounter);
        }
        animator.SetBool("Jump", !isGrounded);

        //Add jump velocity if the player used the jump key and perform a jump
        if (isJumping && (jumpCounter < MAXJUMPS) && !jumpHold)
        {
            rb.velocity = Vector2.up * jumpHeight;
            jumpCounter++;
            animator.SetInteger("JumpCount", jumpCounter);
            jumpHold = true;
        }

        //For a long jump while holding down the button
        if (jumpHold)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpHeight;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
                jumpHold = false;
            }
        }
    }
}