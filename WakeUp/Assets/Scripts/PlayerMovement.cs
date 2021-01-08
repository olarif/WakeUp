using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    //movement
    public float speed = 10;
    private float moveX;
    private bool faceRight;

    //jumping
    public float jumpForce = 15f;
    public LayerMask groundLayer;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    //ground check
    private bool isGrounded;
    public Transform groundCheck;
    public float radius;

    //particle system
    public ParticleSystem dust;

    //dashing
    public float StartDashTimer;
    private float CurrentDashTimer;
    private float DashDirection;
    public float dashSpeed = 5f;
    bool isDashing;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    private void FixedUpdate()
    {
        //move player
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        //check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);

        //Jump multipliers
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        //flip character
        if (!faceRight && moveX < 0)
        {
            Flip();
        }
        else if (faceRight && moveX > 0)
        {
            Flip();
        }

    }

    private void Update()
    {

        animator.SetFloat("Speed", Mathf.Abs(moveX));


        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 dir = new Vector2(x, y);

        //call jump function
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump(); 
        }

        if (!isGrounded)
        {
            animator.SetBool("IsJumping", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Dash(x, y);
        }
    }

    void Dash(float x, float y)
    {
        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x, y).normalized * 30;
    }


    /*

    void Dash()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;
        var direction = (mousePos - this.transform.position);

        rb.velocity = Vector3.zero;

        transform.position += direction * dashSpeed;
    }

    */
    
    void CreateDust()
    {
        dust.Play();
    }

   public void Jump()
    {
        CreateDust();
        rb.velocity = Vector2.up * jumpForce;
    }

    //flip character to facing direction
    void Flip()
    {
        if (isGrounded) CreateDust();
        //swap x rotation of sprite
        faceRight = !faceRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }


}
