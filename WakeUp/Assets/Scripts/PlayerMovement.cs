using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    //movement
    public float speed = 10f;
    private float moveX;
    public float airSpeed = 20f;
    private bool faceRight;

    //jumping
    public LayerMask groundLayer;
    public float jumpForce = 15f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    //grappling hook
    public LineRenderer line;
    public LayerMask mask;
    public Transform grapple;
    DistanceJoint2D joint;
    RaycastHit2D hit;
    Vector3 targetPos;
    Vector2 lookDirection;
    public float maxDistance;
    public float minDistance;
    public float hoistSpeed;
    bool isGrabbed = false;

    //ground check
    public Transform groundCheck;
    private bool isGrounded;
    public float radius;

    //particle system
    public ParticleSystem dust;

    //dashing
    public float StartDashTimer;
    private float CurrentDashTimer;
    private float DashDirection;
    public float dashSpeed = 5f;
    bool isDashing;

    //flying for test purposes
    public bool flying = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
    }

    private void Update()
    {
        //save horizontal and vertical axis in variabled
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //set direction
        Vector2 dir = new Vector2(x, y);

        //call jump function
        if (Input.GetButtonDown("Jump") && isGrounded) Jump();

        //animator
        animator.SetFloat("Speed", Mathf.Abs(moveX));
        if (!isGrounded)     animator.SetBool("IsJumping", true);
        else if (isGrounded) animator.SetBool("IsJumping", false);

        //dash
        if (Input.GetKeyDown(KeyCode.E)) Dash(x, y);

        //grapple script
        if (Input.GetMouseButtonDown(0) && !isGrounded)
        {
            SetRope();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DestroyRope();
        }

        //move up and down the rope
        if (isGrabbed && Input.GetKey("w") && joint.distance < maxDistance)
        {
            joint.distance -= hoistSpeed * Time.deltaTime;
        }

        if (isGrabbed && Input.GetKey("s"))
        {
            if (joint.distance > maxDistance-1) return;
            joint.distance += hoistSpeed * Time.deltaTime;
        }

        if (flying) Fly();
    }

    private void FixedUpdate()
    {
        //render line
        line.SetPosition(0, grapple.position);
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - grapple.position;

        //check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);

        //move player
        moveX = Input.GetAxis("Horizontal");

        //movement on ground
        if (isGrounded)
        {
            MoveOnGround();
            DestroyRope();
        }

        if (!isGrounded && !isGrabbed && !flying)
        {
            rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        }

        //movement while hooked
        if (isGrabbed && !flying)
        {
            MoveInAir();
        }

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
        if      (!faceRight && moveX < 0) Flip();
        else if  (faceRight && moveX > 0) Flip();
    }

    void MoveOnGround() 
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
    }

    void MoveInAir()
    {
        if (moveX > 0)
        {
            Vector2 force = new Vector2(airSpeed, 0);

            rb.AddForce(force, ForceMode2D.Force);
            //rb.velocity = new Vector2(moveX * airSpeed, 10);
        }

        else if (moveX < 0)
        {
            Vector2 force = new Vector2(-airSpeed, 0);

            rb.AddForce(force, ForceMode2D.Force);
            //rb.velocity = new Vector2(moveX * airSpeed, 100);
        }
    }

    //draw rope
    void SetRope()
    {
        line.useWorldSpace = true;
        RaycastHit2D hit = Physics2D.Raycast(grapple.position, lookDirection, maxDistance, mask);
        if (hit) isGrabbed = true;

        if (hit.collider != null)
        {
            joint.enabled = true;
            joint.connectedAnchor = hit.point;
            line.enabled = true;
            line.SetPosition(1, hit.point);
        }
    }

    //delete rope
    void DestroyRope()
    {
        isGrabbed = false;
        joint.enabled = false;
        line.enabled = false;
        line.useWorldSpace = false;
    }
    
    void CreateDust()
    {
        dust.Play();
    }

    void Jump()
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

    void Dash(float x, float y)
    {
        //float dashDistance = 100f;

        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x, y).normalized * 30;
    }

    void Fly()
    {
        float flyMoveX = Input.GetAxis("Horizontal");
        float flyMoveY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(flyMoveX * speed, flyMoveY * speed);
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
}
