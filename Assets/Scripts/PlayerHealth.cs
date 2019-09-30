using System;
using UnityEngine;

public class PlayerHealth: MonoBehaviour, IDamageable
{
    #region SerializeFields

    [SerializeField] private int startingHealth = 100;

    #endregion

    #region NonSerializeFields

    private int currentHealth;

    #endregion

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}