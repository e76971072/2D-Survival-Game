using System;
using Interfaces;
using UnityEngine;

namespace Props
{
    [RequireComponent(typeof(Animator))]
    public abstract class Health : MonoBehaviour, IDamageable
    {
        #region ExposedFields

        public static event Action<Health> OnHealthAdded = delegate { };
        public static event Action<Health> OnHealthRemoved = delegate { };
        public event Action<float> OnHealthPctChanged = delegate { };

        [SerializeField] protected int maxHealth = 100;

        #endregion

        #region NonExposedFields

        private int currentHealth;

        protected int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

                var currentHealthPct = (float) currentHealth / maxHealth;
                OnHealthPctChanged(currentHealthPct);
            }
        }

        #endregion

        public virtual void TakeDamage(int damageAmount)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        private void OnEnable()
        {
            CurrentHealth = maxHealth;
            OnHealthAdded(this);
        }

        protected abstract void Die();

        protected virtual void OnDisable()
        {
            OnHealthRemoved(this);
        }
    }
}