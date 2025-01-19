using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;

    public void ActiveProjectile() {
        lifeTime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float moveSpeed = speed * Time.deltaTime;
        transform.Translate(moveSpeed,0,0);

        lifeTime += Time.deltaTime;
        if (lifeTime > resetTime) {
            gameObject.SetActive(false);
        }
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}
