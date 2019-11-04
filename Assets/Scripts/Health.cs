using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    #region SerializeFields

    [SerializeField] protected int maxHealth = 100;
    
    public static event Action<Health> OnHealthAdded = delegate {  };
    public static event Action<Health> OnHealthRemoved = delegate {  };
    public event Action<float> OnHealthPctChanged = delegate {  };

    #endregion

    #region NonSerializeFields

    protected int currentHealth;

    #endregion
    
    private void OnEnable()
    {
        currentHealth = maxHealth;
        OnHealthAdded(this);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        float currentHealthPct = (float) currentHealth / maxHealth;
        OnHealthPctChanged(currentHealthPct);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        OnHealthRemoved(this);
        Destroy(gameObject);
    }
}