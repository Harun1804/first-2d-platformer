using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Point")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animation")]
    [SerializeField] private Animator anim;

    private Vector2 initScale;
    private bool IsMovingLeft;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool(EnemyAnimationString.IsMoving, false);
    }

    private void Update()
    {
        if (IsMovingLeft) {
            if (enemy.position.x >= leftEdge.position.x) {
                MoveInDirection(-1);
            } else {
                // Change Direction
                ChangeDirection();
            }
        } else {
            if (enemy.position.x <= rightEdge.position.x) {
                MoveInDirection(1);
            } else {
                // Change Direction
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        anim.SetBool(EnemyAnimationString.IsMoving, false);
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration) { 
            IsMovingLeft = !IsMovingLeft;
        }
    }

    private void MoveInDirection(int _direction) {
        idleTimer = 0;

        anim.SetBool(EnemyAnimationString.IsMoving, true);

        // Make enemy face the direction it's moving
        enemy.localScale = new Vector2(Mathf.Abs(initScale.x) * _direction, initScale.y);


        // Move in that direction
        enemy.position = new Vector2(enemy.position.x + Time.deltaTime * _direction * moveSpeed, enemy.position.y);
    }
}
