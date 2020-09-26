using Helpers;
using UnityEngine;

namespace Player
{
    public class AttackedAnimatorHandler : AnimatorHandler
    {
        private readonly int _blink = Animator.StringToHash("Blink");

        public void PlayDamagedAnimation()
        {
            Animator.SetTrigger(_blink);
        }
    }
}