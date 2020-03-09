using Helpers;
using Interfaces;
using UnityEngine;

namespace Attacks
{
    [RequireComponent(typeof(Animator))]
    public abstract class MeleeAttack : MonoBehaviour
    {
        #region ExposedFields

        [SerializeField] protected float timeBetweenAttack;
        [SerializeField] protected int damage;
        [SerializeField] protected float attackRange = 1f;
        [SerializeField] protected LayerMask targetLayerMask;
        [SerializeField] protected Transform attackPosition;

        protected float timer;

        #endregion

        protected virtual void Awake()
        {
            timer = timeBetweenAttack;
        }

        protected virtual void Update()
        {
            timer += Time.deltaTime;
        }

        protected void Attack()
        {
            ResetAttackTime();
            var targetResults = new Collider2D[50];
            var targetCount =
                Physics2D.OverlapCircleNonAlloc(attackPosition.position, attackRange, targetResults, targetLayerMask);
            for (var i = 0; i < targetCount; i++)
            {
                var targetCollider2D = targetResults[i];
                targetCollider2D.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }

        private void ResetAttackTime()
        {
            timer = 0f;
        }
        
        protected abstract bool CantDamage();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPosition.position, attackRange);
        }
    }
}