using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorHandler : MonoBehaviour
    {
        private static readonly int Blink = Animator.StringToHash("Blink");

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayDamagedAnimation()
        {
            animator.SetTrigger(Blink);
        }
    }
}