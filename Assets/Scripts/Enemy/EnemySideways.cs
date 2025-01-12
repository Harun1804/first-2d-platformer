using UnityEngine;

public class EnemySideways : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float movementDistace;
    [SerializeField] private float speed;

    private bool isMovingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistace;
        rightEdge = transform.position.x + movementDistace;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(StringValue.PlayerTag)) {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void Update()
    {
        if (isMovingLeft) {
            if (transform.position.x > leftEdge) {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                isMovingLeft = false;
            }
        } else {
            if (transform.position.x < rightEdge) {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                isMovingLeft = true;
            }
        }
    }
}
