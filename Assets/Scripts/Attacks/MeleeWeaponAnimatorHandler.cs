using Helpers;
using UnityEngine;

namespace Attacks
{
    public class MeleeWeaponAnimatorHandler : AnimatorHandler
    {
        private readonly int _attackParameter = Animator.StringToHash("Attack");
        
        public void PlayAttackAnimation()
        {
            Animator.SetTrigger(_attackParameter);
        }
    }
}