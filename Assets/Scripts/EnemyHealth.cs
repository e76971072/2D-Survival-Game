using System;
using Pathfinding;
using UnityEngine;

public class EnemyHealth : Health
{
    public static event Action EnemyHit = delegate { };

    #region SerializeFields

    [SerializeField] private float deadWaitTime;

    #endregion

    #region NonSerializeFields

    private Animator animator;
    private static readonly int Blink = Animator.StringToHash("Blink");

    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        animator.SetTrigger(Blink);
        EnemyHit();
    }

    protected override void Die()
    {
        HideEnemy();
        RemoveHealthBar();
        Destroy(transform.gameObject, deadWaitTime);
    }

    private void HideEnemy()
    {
        GetComponent<AIPath>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
    }

    private void FadeEnemySprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 0.2f;
        spriteRenderer.color = spriteColor;
    }
}