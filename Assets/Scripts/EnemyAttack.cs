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

    private void OnCollisionStay2D()
    {
        if (!CanDamage()) return;
        
        Attack();
        ResetAttackTime();
    }
}