using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 20f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    private float wallJumpCooldown;
    private float horizontalInput;

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis(StringValue.AxisHorizontal);

        // Flip the player sprite based on the direction they are moving
        if (horizontalInput > 0.01f) {
            transform.localScale = Vector2.one;
        } else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector2(-1, 1);
        }

        // Play the animation based on the player's movement
        anim.SetBool(StringValue.IsRun, horizontalInput != 0);
        anim.SetBool(StringValue.IsGrounded, IsGrounded());

        // Wall Jump
        if (wallJumpCooldown > 0.2f) {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocityY);

            if (OnWall() && !IsGrounded()) {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            } else {
                body.gravityScale = 7;
            }

            if (Input.GetKey(KeyCode.Space)) {
                Jump();
            }
        } else {
            wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump() 
    {
        if (IsGrounded()) {
            body.linearVelocity = new Vector2(body.linearVelocityX, jumpPower);
            anim.SetTrigger(StringValue.Jump);
        } else if (OnWall() && !IsGrounded()) {
            if (horizontalInput == 0) {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);
            } else {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;
        }
    }

    private bool IsGrounded() 
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit2D.collider != null;
    }
}
