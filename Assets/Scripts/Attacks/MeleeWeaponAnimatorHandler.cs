using Helpers;
using UnityEngine;

namespace Attacks
{
    public class MeleeWeaponAnimatorHandler : AnimatorHandler
    {
        private readonly int attackParameter = Animator.StringToHash("Attack");
        
        public void PlayAttackAnimation()
        {
            animator.SetTrigger(attackParameter);
        }
    }
}