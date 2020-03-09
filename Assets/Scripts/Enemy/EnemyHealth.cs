using System;
using Attacks;
using Cinemachine;
using Helpers;
using Interfaces;
using Pathfinding;
using Player;
using Props;
using UnityEngine;
using UnityEngine.Audio;

namespace Enemy
{
    [RequireComponent(typeof(IAudioHandler))]
    [RequireComponent(typeof(AttackedAnimatorHandler))]
    [RequireComponent(typeof(DiedHandler))]
    public class EnemyHealth : Health
    {
        public static event Action OnEnemyHit;

        private IAudioHandler audioPlayer;
        private DiedHandler diedHandler;
        private AttackedAnimatorHandler enemyAnimatorHandler;

        protected virtual void Awake()
        {
            diedHandler = GetComponent<DiedHandler>();
            enemyAnimatorHandler = GetComponent<AttackedAnimatorHandler>();
            audioPlayer = GetComponent<IAudioHandler>();
        }

        public override void TakeDamage(int damageAmount)
        {
            audioPlayer.PlayAudioSource();
            enemyAnimatorHandler.PlayDamagedAnimation();
            OnEnemyHit?.Invoke();
            base.TakeDamage(damageAmount);
        }

        protected override void Die()
        {
            diedHandler.Die();
        }
    }
}