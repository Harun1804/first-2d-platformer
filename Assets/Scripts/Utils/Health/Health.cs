using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool isDead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0) {
            // Player hurt
            anim.SetTrigger(StringValue.Hurt);
        } else {
            // Player dead
            if (!isDead) {                
                anim.SetTrigger(StringValue.Die);
                GetComponent<PlayerMovement>().enabled = false;
                isDead = true;
            }
        }
    }

    public void Heal(float _healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + _healAmount, 0, startingHealth);
    }
}
