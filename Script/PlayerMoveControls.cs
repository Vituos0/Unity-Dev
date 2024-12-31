using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed;
    public float JumpForce;
    public float fallingForce;
    private float defaultGravityForce = 2;
    private float GravityBelt=8;

    private GatherInput gI;
    private Rigidbody2D rb;
    private Animator anim;

    private float direction = 1;

    public float rayLength;
    public LayerMask groundLayer;
    public Transform leftPoint;

    public bool grounded;
    //private bool doubleJump;
    public int additinalJumps = 2;
    private int resetJumpsNumber;
    

    

    void Start()
    {
        gI = GetComponent<GatherInput>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        resetJumpsNumber=additinalJumps;

    }


    void Update()
    {
        SetAnimatorValue();
    }

    private void FixedUpdate()
    {   
        CheckStatus();
        Movement();
        Jumping();
        Falling();

    }
    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        if (leftCheckHit)
        {
            grounded = true;
            //doubleJump = false;
            additinalJumps = resetJumpsNumber;
        }
        else
        {
            grounded = false;
        }
        SeeRay(leftCheckHit);
    }
    private void SeeRay(RaycastHit2D leftCheckHit)
    {
        Color color1= leftCheckHit?Color.red:Color.green;
        Debug.DrawRay(leftPoint.position, Vector2.down * rayLength, color1);
    }

    private void Movement()
    {
        rb.velocity = new Vector2(speed * gI.valueX, rb.velocity.y);
        Flip();
    }

    private void Jumping()
    {
        if (gI.JumpInput)
        {
            if (grounded)
            {
                rb.velocity = new Vector2(gI.valueX * speed, JumpForce);
                //doubleJump = true;

               
            }
            else if (additinalJumps>0)
            {
                rb.velocity = new Vector2(gI.valueX * speed, JumpForce);
                //doubleJump = false;
                additinalJumps -= 1;
            }
        } 
        gI.JumpInput = false;
    }
    private void Flip()
    {
        if (gI.valueX*direction < 0) 
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            direction *= -1;
            
        }
    }
    private void Falling()
    {
        if (!grounded)
        {
            if (gI.FallInput)
            {
                rb.gravityScale= GravityBelt;
            }
            else
            {
                rb.gravityScale= defaultGravityForce;
            }
        }
      
    }
    private void SetAnimatorValue()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("vSpeed", rb.velocity.y);
        anim.SetBool("Grounded", grounded);

    }
}
