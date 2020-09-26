using Helpers;
using Interfaces;
using PickupsTypes;
using Props;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health, IHealable
    {
        private AttackedAnimatorHandler _animatorHandler;
        
        private void Awake()
        {
            _animatorHandler = GetComponent<AttackedAnimatorHandler>();
                            
            GameManager.OnGameLost += DisableOnDead;
            HealthPickups.OnHealthPickedUp += Heal;
        }

        protected override void Die()
        {
            UIManager.Instance.SetLosingReasonText("You Died!");
            GameManager.Instance.GameLost();
        }

        public override void TakeDamage(int damageAmount)
        {
            _animatorHandler.PlayDamagedAnimation();
            base.TakeDamage(damageAmount);
        }

        private void DisableOnDead()
        {
            GetComponent<Collider2D>().enabled = false;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GameManager.OnGameLost -= DisableOnDead;
        }

        public void Heal(int healAmount)
        {
            CurrentHealth += healAmount;
        }
    }
}