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

        #region NonExposedFields

        private Animator animator;
        private readonly int attackParameter = Animator.StringToHash("Attack");

        #endregion

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
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
                targetCollider2D.GetComponent<IHealth>().ModifyHealth(-damage);
            }
        }

        private void ResetAttackTime()
        {
            timer = 0f;
        }


        protected void PlayAttackAnimation()
        {
            animator.SetTrigger(attackParameter);
        }

        protected virtual bool CantDamage()
        {
            return !GameManager.Instance.playerInput.canShoot || !(timer >= timeBetweenAttack);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPosition.position, attackRange);
        }
    }
}