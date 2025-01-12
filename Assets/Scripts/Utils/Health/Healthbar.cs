using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealthbar;
    [SerializeField] private Image currentHealthbar;

    void Start()
    {
        totalHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }

    void Update()
    {
        currentHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
}
