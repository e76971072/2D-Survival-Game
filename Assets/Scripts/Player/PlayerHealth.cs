using Helpers;
using PickupsTypes;
using Props;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health
    {
        private void Awake()
        {
            GameManager.OnGameLost += DisableOnDead;
            HealthPickups.OnHealthPickedUp += ModifyHealth;
        }

        protected override void Die()
        {
            GameManager.Instance.GameLost();
            base.Die();
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
    }
}