using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float, float> OnHealth;
    public event Action OnDeath;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        if (currentHealth > 0)
            OnHealth?.Invoke(currentHealth, maxHealth);
    }

    public void Heal(float heal)
    {
        currentHealth += heal;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth >= 0)
            OnHealth?.Invoke(currentHealth, maxHealth);
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }

}
