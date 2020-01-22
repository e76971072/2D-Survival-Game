using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyMeleeAttack : MeleeAttack
{
    protected override bool CanDamage()
    {
        return Time.time >= nextTimeToAttack;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() == null) return;
        if (!CanDamage()) return;

        Attack();
    }
}