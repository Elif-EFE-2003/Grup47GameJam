using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    public float jumpForce = 6f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded;

    [Header("Ground Check Ayarlar�")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;

    private Vector3 originalScale;

    public SquirrelSceneManager squirrelSceneManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
        animator.speed = 0.2f; // %50 yavaşlatır

    }

    private bool jump = false;
    void Update()
    {
        if(squirrelSceneManager.gameStarted == false)
        {
            return;
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        
        if (moveInput > 0)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        
        if (animator != null)
        {
            animator.SetBool("isRunning", moveInput != 0);
            animator.SetBool("isGrounded", isGrounded);
        }

        
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
        isGrounded = hit.collider != null;
        Debug.DrawRay(groundCheck.position, Vector2.down * 0.1f, Color.red);


        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded) jump = true;

    }

    void FixedUpdate()
    {
        
        if (jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator?.SetTrigger("jump");
            jump = false;
        }
    }



    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}



