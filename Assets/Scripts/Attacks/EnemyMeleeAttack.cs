using Player;
using UnityEngine;

namespace Attacks
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyMeleeAttack : MeleeAttack
    {
        protected override bool CantDamage()
        {
            return !(timer >= timeBetweenAttack);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<PlayerHealth>() == null) return;
            if (CantDamage()) return;

            Attack();
        }
    }
}