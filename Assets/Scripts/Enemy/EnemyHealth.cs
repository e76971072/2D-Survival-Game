using System;
using Attacks;
using Cinemachine;
using Pathfinding;
using Props;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : Health
    {
        public static event Action OnEnemyHit = delegate { };

        private Animator animator;
        private static readonly int Blink = Animator.StringToHash("Blink");
        private CinemachineImpulseSource impulseSource;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        public override void ModifyHealth(int damage)
        {
            animator.SetTrigger(Blink);
            OnEnemyHit();
            impulseSource.GenerateImpulse();
            base.ModifyHealth(damage);
        }

        protected override void Die()
        {
            HideEnemy();
            base.Die();
        }

        private void HideEnemy()
        {
            GetComponent<AIPath>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            if (GetComponent<MeleeAttack>())
            {
                GetComponent<MeleeAttack>().enabled = false;
                return;
            }

            GetComponent<RangeAttack>().enabled = false;
        }
    }
}