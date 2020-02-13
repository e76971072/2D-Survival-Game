using System;
using Attacks;
using Cinemachine;
using Interfaces;
using Pathfinding;
using Props;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(DiedHandler))]
    public class EnemyHealth : Health
    {
        public static event Action OnEnemyHit = delegate { };

        private Animator animator;
        private static readonly int Blink = Animator.StringToHash("Blink");

        private DiedHandler diedHandler;
        private AudioSource audioSource;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            diedHandler = GetComponent<DiedHandler>();
            audioSource = GetComponent<AudioSource>();
        }

        public override void ModifyHealth(int damage)
        {
            animator.SetTrigger(Blink);
            audioSource.Stop();
            audioSource.Play();
            OnEnemyHit();
            base.ModifyHealth(damage);
        }

        protected override void Die()
        {
            diedHandler.Die();
        }
    }
}