using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("Spikehead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    private float checkTimer;
    private bool isAttacking;
    private Vector2 destination;
    private Vector2[] directions = new Vector2[4];

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (isAttacking) {
            transform.Translate(destination * Time.deltaTime * speed);
        } else {
            checkTimer += Time.deltaTime;
            if (checkTimer >= checkDelay) {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirection();
        for (int i = 0; i < directions.Length; i++) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            if (hit.collider != null && !isAttacking) {
                destination = directions[i];
                isAttacking = true;
                checkTimer = 0;
            }
        }
    }

    private void Stop() {
        destination = transform.position;
        isAttacking = false;
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop();
    }

    private void CalculateDirection() {
        directions[0] = transform.right * range; // Right
        directions[1] = -transform.right * range; // Left
        directions[2] = transform.up * range; // Up
        directions[3] = -transform.up * range; // Down
    }
}
