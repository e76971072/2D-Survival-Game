using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyAttack : MeleeAttack
{
    private void ResetAttackTime()
    {
        timer = 0f;
    }

    protected override bool CanDamage()
    {
        return timer >= timeBetweenAttack;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!CanDamage()) return;
        
        Attack();
        ResetAttackTime();
    }
}