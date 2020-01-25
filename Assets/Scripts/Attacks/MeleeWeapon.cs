using Helpers;
using UnityEngine;

namespace Attacks
{
    public class MeleeWeapon : MeleeAttack
    {
        protected override void Awake()
        {
            GameManager.OnGameLost += DisableOnDead;
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            if (CantDamage()) return;

            PlayAttackAnimation();
            Attack();
        }

        private void DisableOnDead()
        {
            GetComponent<MeleeWeapon>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }

        private void OnDisable()
        {
            timer = timeBetweenAttack;
        }

        private void OnDestroy()
        {
            GameManager.OnGameLost -= DisableOnDead;
        }
    }
}