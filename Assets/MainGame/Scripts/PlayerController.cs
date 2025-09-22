using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
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
    private bool FaceRight = true;
    private Animator anim;

    private int isWalkingID;
    private int isJumpingID;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        isWalkingID = Animator.StringToHash("Walking");
        isJumpingID = Animator.StringToHash("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            StartCoroutine(Jump());
        }
    }


    #region Wait Animation
    IEnumerator Jump()
    {
        anim.SetTrigger(isJumpingID);
        yield return new WaitForSeconds(0.3f);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
    #endregion
    #region Movement
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        rb.linearVelocity = new Vector2(horizontal, rb.linearVelocity.y);
        if ((horizontal > 0 && !FaceRight) || (horizontal < 0 && FaceRight))
        {
            Flip();
        }
        if (math.abs(rb.linearVelocity.x) > 0.1f)
        {
            anim.SetBool(isWalkingID, true);
        }
        else
        {
            anim.SetBool(isWalkingID, false);
        }
    }
    #endregion

    #region Flip character
    public bool isFacingRight()
    {
        return FaceRight;
    }

    private void Flip()
    {
        FaceRight = !FaceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    #endregion

    #region Check is on ground
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    #endregion
}
