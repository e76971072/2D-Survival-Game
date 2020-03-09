using System;
using Data;
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
        public event Action<float> OnHealthPctChanged;

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
                OnHealthPctChanged?.Invoke(currentHealthPct);
            }
        }

        #endregion

        private void Awake()
        {
        }

        private void OnEnable()
        {
            CurrentHealth = maxHealth;
            OnHealthAdded(this);
        }

        public virtual void TakeDamage(int damageAmount)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        protected abstract void Die();

        protected virtual void OnDisable()
        {
            OnHealthRemoved(this);
        }
    }
}