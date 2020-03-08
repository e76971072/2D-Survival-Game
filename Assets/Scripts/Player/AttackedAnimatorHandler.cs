using Helpers;
using UnityEngine;

namespace Player
{
    public class AttackedAnimatorHandler : AnimatorHandler
    {
        private readonly int blink = Animator.StringToHash("Blink");

        public void PlayDamagedAnimation()
        {
            animator.SetTrigger(blink);
        }
    }
}