using System;
using Attacks;
using Cinemachine;
using Helpers;
using Interfaces;
using Pathfinding;
using Props;
using UnityEngine;
using UnityEngine.Audio;

namespace Enemy
{
    [RequireComponent(typeof(IAudioHandler))]
    [RequireComponent(typeof(AnimatorHandler))]
    [RequireComponent(typeof(DiedHandler))]
    public class EnemyHealth : Health
    {
        public static event Action OnEnemyHit;

        private IAudioHandler audioPlayer;
        private DiedHandler diedHandler;
        private AnimatorHandler animatorHandler;

        protected virtual void Awake()
        {
            diedHandler = GetComponent<DiedHandler>();
            animatorHandler = GetComponent<AnimatorHandler>();
            audioPlayer = GetComponent<IAudioHandler>();
        }

        public override void TakeDamage(int damageAmount)
        {
            audioPlayer.PlayAudioSource();
            animatorHandler.PlayDamagedAnimation();
            OnEnemyHit?.Invoke();
            base.TakeDamage(damageAmount);
        }

        protected override void Die()
        {
            diedHandler.Die();
        }
    }
}