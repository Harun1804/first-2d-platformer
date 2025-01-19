using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(StringValue.PlayerTag)) {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
