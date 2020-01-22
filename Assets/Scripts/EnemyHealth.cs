using System;
using Cinemachine;
using Pathfinding;
using UnityEngine;

public class EnemyHealth : Health
{
    public static event Action EnemyHit = delegate { };

    #region NonSerializeFields

    private Animator animator;
    private static readonly int Blink = Animator.StringToHash("Blink");
    private CinemachineImpulseSource impulseSource;

    #endregion

    private void Awake()
    {
        GameManager.OnGameLost += ClearEvent;
        GameManager.OnGameReload += ClearEvent;
        
        animator = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public override void ModifyHealth(int damage)
    {
        animator.SetTrigger(Blink);
        EnemyHit();
        impulseSource.GenerateImpulse();
        base.ModifyHealth(damage);
    }

    protected override void Die()
    {
        HideEnemy();
        base.Die();
    }

    private void HideEnemy()
    {
        GetComponent<AIPath>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        if (GetComponent<MeleeAttack>())
        {
            GetComponent<MeleeAttack>().enabled = false;
            return;
        }

        GetComponent<RangeAttack>().enabled = false;
    }

    private void FadeEnemySprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = 0.2f;
        spriteRenderer.color = spriteColor;
    }

    private void ClearEvent()
    {
        Debug.Log(EnemyHit == null);
        if (EnemyHit == null) return;
        
        EnemyHit = null;
        GameManager.OnGameLost -= ClearEvent;
        GameManager.OnGameReload -= ClearEvent;
        Debug.Log("EnemyHit event cleared!");
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameManager.OnGameLost -= ClearEvent;
        GameManager.OnGameReload -= ClearEvent;
    }
}