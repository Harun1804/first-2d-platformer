using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private float direction;
    private bool isHit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isHit) {
            return;
        }

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) {
            Deactive();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;
        boxCollider2D.enabled = false;
        anim.SetTrigger(StringValue.Explode);

        if (collision.tag.Equals(StringValue.EnemyTag)) {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction) 
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        isHit = false;
        boxCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction) {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    }
    private void Deactive() 
    {
        gameObject.SetActive(false);
    }
}
