using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth { get; private set; }

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
