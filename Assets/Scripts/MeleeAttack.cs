using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class MeleeAttack : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] protected float attackRate;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRange = 1f;
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] protected Transform attackPosition;

    #endregion

    #region NonSerializeFields

    protected float nextTimeToAttack;
    private Animator animator;
    private readonly int attackParameter = Animator.StringToHash("Attack");

    #endregion

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        nextTimeToAttack = Time.time + 1f / attackRate;
    }

    protected void Attack()
    {
        Collider2D[] targetResults = new Collider2D[50];
        var targetCount =
            Physics2D.OverlapCircleNonAlloc(attackPosition.position, attackRange, targetResults, targetLayerMask);
        for (var i = 0; i < targetCount; i++)
        {
            Collider2D targetCollider2D = targetResults[i];
            targetCollider2D.GetComponent<IHealth>().ModifyHealth(-damage);
        }
    }

    protected void PlayAttackAnimation()
    {
        animator.SetTrigger(attackParameter);
    }

    protected virtual bool CanDamage()
    {
        return Input.GetButton("Fire1") && Time.time >= nextTimeToAttack;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}