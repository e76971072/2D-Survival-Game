using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour, IHealth
{
    #region SerializeFields

    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected float deadWaitTime = 2f;

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

    public virtual void ModifyHealth(int damage)
    {
        currentHealth += damage;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        var currentHealthPct = (float) currentHealth / maxHealth;
        OnHealthPctChanged(currentHealthPct);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject, deadWaitTime);
    }

    protected void OnDisable()
    {
        OnHealthRemoved(this);
    }
}