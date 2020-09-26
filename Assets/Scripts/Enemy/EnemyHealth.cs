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

        private IAudioHandler _audioPlayer;
        private DiedHandler _diedHandler;
        private AttackedAnimatorHandler _enemyAnimatorHandler;

        protected virtual void Awake()
        {
            _diedHandler = GetComponent<DiedHandler>();
            _enemyAnimatorHandler = GetComponent<AttackedAnimatorHandler>();
            _audioPlayer = GetComponent<IAudioHandler>();
        }

        public override void TakeDamage(int damageAmount)
        {
            _audioPlayer.PlayAudioSource();
            _enemyAnimatorHandler.PlayDamagedAnimation();
            OnEnemyHit?.Invoke();
            base.TakeDamage(damageAmount);
        }

        protected override void Die()
        {
            _diedHandler.Die();
        }
    }
}