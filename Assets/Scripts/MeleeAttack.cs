using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class MeleeAttack : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] protected float timeBetweenAttack;
    [SerializeField] protected int damage;
    [SerializeField] protected float hitRadius = 1f;
    [SerializeField] protected LayerMask targetLayerMask;

    #endregion

    #region NonSerializeFields

    protected float timer;
    private Collider2D[] targetResults;
    private Animator animator;
    private readonly int attackParameter = Animator.StringToHash("Attack");

    #endregion

    protected virtual void Awake()
    {
        targetResults = new Collider2D[50];
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
    }

    protected void Attack()
    {
        var targetCount =
            Physics2D.OverlapCircleNonAlloc(transform.position, hitRadius, targetResults, targetLayerMask);
        for (var i = 0; i < targetCount; i++)
        {
            var targetCollider2D = targetResults[i];
            targetCollider2D.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }

    private bool IsIdle()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }

    protected void PlayAttackAnimation()
    {
        animator.SetTrigger(attackParameter);
    }

    protected bool CanDamage()
    {
        return Input.GetButton("Fire1") && timer >= timeBetweenAttack && IsIdle();
    }
}