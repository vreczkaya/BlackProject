using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Slider healthBar;

    [SerializeField]
    private Health health;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = health.MaxHealth;
    }

    private void Update()
    {
        healthBar.value = health.CurrentHealth;
    }
}
