using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private bool IsHit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ActiveProjectile() {
        IsHit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

    private void Update()
    {
        if (IsHit) {
            return;
        }
        float moveSpeed = speed * Time.deltaTime;
        transform.Translate(moveSpeed,0,0);

        lifeTime += Time.deltaTime;
        if (lifeTime > resetTime) {
            gameObject.SetActive(false);
        }
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        IsHit = true;
        base.OnTriggerEnter2D(collision);
        boxCollider.enabled = false;

        if (anim != null) {
            anim.SetTrigger(StringValue.Explode);
        } else {
            gameObject.SetActive(false);
        }
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }
}
