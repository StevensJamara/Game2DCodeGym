using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[AddComponentMenu("PlayerPlays/PlayerController")]

public class PlayerController: MonoBehaviour
{
    [Header("Player Settings")]
    public LayerMask groundLayer;
    public float speed = 5.0f;
    public float jumpForce = 15.0f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown("Space") && IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Move()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            float move = Input.GetAxis("Horizontal") * speed;
            rb.linearVelocity = new Vector2(move, rb.linearVelocity.y);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
