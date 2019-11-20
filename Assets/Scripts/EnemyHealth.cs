using System;
using UnityEngine;

public class EnemyHealth : Health
{
    public static event Action EnemyHit = delegate { };

    #region SerializeFields

    [SerializeField] private float deadWaitTime;

    #endregion

    #region NonSerializeFields

    private Collider2D enemyCollider;
    private Animator animator;
    private static readonly int Blink = Animator.StringToHash("Blink");

    #endregion

    private void Awake()
    {
        enemyCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        animator.SetTrigger(Blink);
//        Score.Instance.EnemyHit();
        EnemyHit();
    }

    protected override void Die()
    {
        RemoveHealthBar();
        enemyCollider.enabled = false;
        Destroy(transform.parent.gameObject, deadWaitTime);
    }
}