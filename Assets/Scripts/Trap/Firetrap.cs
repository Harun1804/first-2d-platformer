using System.Collections;
using UnityEngine;

public class Firetrap : EnemyDamage
{
    [Header("Firetrap Times")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool isTriggered;
    private bool isActive;

    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHealth != null && isActive) {
            playerHealth.TakeDamage(damage);
        }
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(StringValue.PlayerTag)) {
            playerHealth = collision.GetComponent<Health>();
            if (!isTriggered) {
                // trigger the firetrap
                StartCoroutine(ActivateFiretrap());
            }

            if (isActive) {
                base.OnTriggerEnter2D(collision);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(StringValue.PlayerTag)) {
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFiretrap() {
        #region Trigger Firetrap
        isTriggered = true;
        spriteRenderer.color = Color.red;
        #endregion

        #region Activate Firetrap
        yield return new WaitForSeconds(activationDelay);
        spriteRenderer.color = Color.white;
        isActive = true;
        anim.SetBool(TrapAnimationString.IsFiretrapActive, true);
        #endregion

        #region Deactivate Firetrap
        yield return new WaitForSeconds(activeTime);
        isActive = false;
        isTriggered = false;
        anim.SetBool(TrapAnimationString.IsFiretrapActive, false);
        #endregion
    }
}
