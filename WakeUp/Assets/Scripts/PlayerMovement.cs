using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    //movement
    public float speed = 10f;
    private float moveX;
    public float airSpeed = 70f;
    private bool faceRight;
    public Transform spawnPoint;

    //jumping
    public LayerMask groundLayer;
    public float jumpForce = 15f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private bool jumpJustPressed;
    private bool jumpHeld;

    //grappling hook
    public LineRenderer line;
    public LayerMask mask;
    public Transform grapple;
    DistanceJoint2D joint;
    public float grappleSpeed = 45f;
    RaycastHit2D hit;
    Vector3 targetPos;
    Vector2 lookDirection;
    public float maxDistance;
    public float hoistSpeed;
    bool isGrabbed = false;
    bool setRope;

    //ground check
    public Transform groundCheck;
    private bool isGrounded;
    private float radius = 0.5f;

    //Animation states
    public Animator animator;
    private string currentState;
    const string IdleAnim = "Idle";
    const string RunAnim = "Run";
    const string JumpStartAnim = "JumpStart";
    const string JumpFallAnim = "JumpFall";
    const string JumpLandAnim = "JumpLand";

    //particle system
    public ParticleSystem dust;

    //flying for test purposes
    public bool flying = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
        setRope = false;
    }

    private void Update()
    {
        //call jump function
        if (Input.GetButtonDown("Jump") && isGrounded) Jump();
        jumpJustPressed = Input.GetButtonDown("Jump");
        jumpHeld = Input.GetButton("Jump");

        if (isGrabbed && Input.GetMouseButtonUp(0))
        {
            RopeLaunch();
        }

        //grapple script
        if (Input.GetMouseButtonDown(0) && !isGrounded)
        {
            setRope = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            setRope = false;
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

        //toggle flying
        if (Input.GetKeyDown("f"))
        {
            flying = !flying;
        }

        if (flying) Fly();
    }

    private void FixedUpdate()
    {
        Vector2 velY = new Vector2(0, rb.velocity.y);
        Vector2 velX = new Vector2(0, rb.velocity.x);

        if (setRope)
        {
            SetRope();
        }

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

        //movement while hooked
        if (isGrabbed && !flying)
        {
            GrappleInAir();
        }

        if (!isGrabbed && !isGrounded)
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

        //Animation states
        if (isGrounded && moveX > 0)
        {
            ChangeAnimationState(RunAnim);
        }
        else if (isGrounded && moveX < 0)
        {
            ChangeAnimationState(RunAnim);
        }
        else if (isGrounded && moveX == 0)
        {
            ChangeAnimationState(IdleAnim);
        }
        else if (!isGrounded && rb.velocity.y > 0)
        {
            ChangeAnimationState(JumpStartAnim);
        }
        else if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnimationState(JumpFallAnim);
        }
    }

    void ChangeAnimationState(string newState)
    {
        //stop the same animation form interrupting itself
        if (currentState == newState) return;

        //play the animation
        animator.Play(newState);

        //reassign the current state
        currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "coin")
        {
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Bell")
        {
            flying = false;
            transform.position = spawnPoint.transform.position;
        }

        if (collision.tag == "dead")
        {
            PlayerDead();
        }
    }

    void PlayerDead()
    {
        var gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager1>();
        transform.position = gm.lastCheckPointPos;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void MoveOnGround() 
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
    }

    void GrappleInAir()
    {
        if (moveX > 0)
        {
            Vector2 force = new Vector2(grappleSpeed, 0);
            rb.AddForce(force, ForceMode2D.Force);
        }

        else if (moveX < 0)
        {
            Vector2 force = new Vector2(-grappleSpeed, 0);
            rb.AddForce(force, ForceMode2D.Force);
        }
    }

    void MoveInAir()
    {
        if (moveX > 0)
        {
            rb.velocity = new Vector2(moveX * airSpeed, rb.velocity.y);
        }

        else if (moveX < 0)
        {
            rb.velocity = new Vector2(moveX * airSpeed, rb.velocity.y);
        }
    }

    void RopeLaunch()
    {
        if (moveX > 0)
        {
            Vector2 force = new Vector2(airSpeed*3, 10);
            rb.AddForce(force, ForceMode2D.Impulse);
        }

        else if (moveX < 0)
        {
            Vector2 force = new Vector2(-airSpeed*3, 10);
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

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

        setRope = false;
    }

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

    void Fly()
    {
        float flyMoveX = Input.GetAxis("Horizontal");
        float flyMoveY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(flyMoveX * speed, flyMoveY * speed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2((flyMoveX * speed) * 2, (flyMoveY * speed) * 2);
        }
    }
}
