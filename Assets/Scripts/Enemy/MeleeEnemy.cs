using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;


    // Reference
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (IsPlayerInSight()) {
            if (cooldownTimer >= attackCooldown) {
                cooldownTimer = 0;
                anim.SetTrigger(EnemyAnimationString.IsMeleeAttack);
            }
        }

        if (enemyPatrol != null) { 
            enemyPatrol.enabled = !IsPlayerInSight();
        }
    }

    private bool IsPlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector2(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y),
            0, 
            Vector2.left,
            0,
            playerLayer
        );

        if (hit.collider != null) {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector2(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y)
        );
    }

    private void DamagePlayer() {
        if (IsPlayerInSight()) {
            playerHealth.TakeDamage(damage);
        }
    }
}
