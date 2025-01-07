using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody2D body;
    private Animator anim;

    private bool isGrounded;

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocityY);

        // Flip the player sprite based on the direction they are moving
        if (horizontalInput > 0.01f) {
            transform.localScale = Vector2.one;
        } else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded) {
            Jump();
        }

        // Play the animation based on the player's movement
        anim.SetBool("IsRun", horizontalInput != 0);
        anim.SetBool("IsGrounded", isGrounded);
    }

    private void Jump() 
    {
        body.linearVelocity = new Vector2(body.linearVelocityX, speed);
        anim.SetTrigger("Jump");
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }
}
