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
        private void Awake()
        {
            GameManager.OnGameLost += DisableOnDead;
            HealthPickups.OnHealthPickedUp += Heal;
        }

        protected override void Die()
        {
            UIManager.Instance.SetLosingReasonText("You Died!");
            GameManager.Instance.GameLost();
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