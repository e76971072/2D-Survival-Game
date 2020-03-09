using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealthWithShield : EnemyHealth, IShieldable
    {
        [SerializeField] private int maxShield = 100;

        private int currentShield = 100;

        public int MaxShield => maxShield;

        protected override void Awake()
        {
            CurrentShield = MaxShield;
            base.Awake();
        }

        public int CurrentShield
        {
            get => currentShield;
            private set
            {
                currentShield = value;
                currentShield = Mathf.Clamp(currentShield, 0, maxShield);
            }
        }

        public bool IsShieldEmpty()
        {
            return CurrentShield == 0;
        }

        public override void TakeDamage(int damageAmount)
        {
            if (IsShieldEmpty())
            {
                base.TakeDamage(damageAmount);
                return;
            }

            CurrentShield -= damageAmount;
        }
    }
}