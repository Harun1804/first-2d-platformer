using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool isInvulnerable;

    public float currentHealth { get; private set; }
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool isDead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (isInvulnerable) {
            return;
        }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0) {
            // Player hurt
            anim.SetTrigger(StringValue.Hurt);
            StartCoroutine(Invunerability());
        } else {
            // Player dead
            if (!isDead) {                
                anim.SetTrigger(StringValue.Die);

                foreach (Behaviour component in components) {
                    component.enabled = false;
                }

                isDead = true;
            }
        }
    }

    public void Heal(float _healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + _healAmount, 0, startingHealth);
    }

    private IEnumerator Invunerability() {
        isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(7, 8, true);

        for (int i = 0; i < numberOfFlashes; i++) {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        isInvulnerable = false;
    }

    private void Deactive() {
        gameObject.SetActive(false);
    }
}
