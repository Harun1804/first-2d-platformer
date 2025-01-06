using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed = 10f;

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocityY);

        if (Input.GetKey(KeyCode.Space)) {
            body.linearVelocity = new Vector2(body.linearVelocityX, speed);
        }
    }
}
