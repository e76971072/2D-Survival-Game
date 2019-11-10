using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour, IDamageable
{
    #region SerializeFields

    [SerializeField] protected int maxHealth = 100;

    public static event Action<Health> OnHealthAdded = delegate { };
    public static event Action<Health> OnHealthRemoved = delegate { };
    public event Action<float> OnHealthPctChanged = delegate { };

    #endregion

    #region NonSerializeFields

    private int currentHealth;

    #endregion

    private void OnEnable()
    {
        currentHealth = maxHealth;
        OnHealthAdded(this);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        var currentHealthPct = (float) currentHealth / maxHealth;
        OnHealthPctChanged(currentHealthPct);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected void RemoveHealthBar()
    {
        OnHealthRemoved(this);
    }

    protected virtual void Die()
    {
        RemoveHealthBar();
        Destroy(gameObject);
    }
}