using UnityEngine;

public class CollectableHealth : MonoBehaviour
{
    [SerializeField] private float healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(StringValue.PlayerTag)) {
            collision.GetComponent<Health>().Heal(healAmount);
            gameObject.SetActive(false);
        }
    }
}
