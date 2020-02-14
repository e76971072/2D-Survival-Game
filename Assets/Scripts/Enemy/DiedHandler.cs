using Attacks;
using Pathfinding;
using UnityEngine;

namespace Enemy
{
    public abstract class DiedHandler : MonoBehaviour
    {
        [SerializeField] protected float timeToDestroy;
        
        public virtual void Die()
        {
            DisableMovementAndAttack();
        }

        private void DisableMovementAndAttack()
        {
            GetComponent<AIPath>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            if (GetComponent<MeleeAttack>())
            {
                GetComponent<MeleeAttack>().enabled = false;
                return;
            }

            GetComponent<RangeAttack>().enabled = false;
        }
    }
}