using System;
using Interfaces;
using UnityEngine;

namespace Props
{
    [RequireComponent(typeof(Animator))]
    public abstract class Health : MonoBehaviour, IHealth
    {
        #region ExposedFields

        public static event Action<Health> OnHealthAdded = delegate { };
        public static event Action<Health> OnHealthRemoved = delegate { };
        public event Action<float> OnHealthPctChanged = delegate { };

        [SerializeField] protected int maxHealth = 100;

        #endregion

        #region NonExposedFields

        private int currentHealth;

        #endregion

        public virtual void ModifyHealth(int damage)
        {
            currentHealth += damage;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            var currentHealthPct = (float) currentHealth / maxHealth;
            OnHealthPctChanged(currentHealthPct);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void OnEnable()
        {
            currentHealth = maxHealth;
            OnHealthAdded(this);
        }

        protected abstract void Die();

        protected virtual void OnDisable()
        {
            OnHealthRemoved(this);
        }
    }
}