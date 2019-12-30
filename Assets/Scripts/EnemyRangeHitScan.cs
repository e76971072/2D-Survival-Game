using UnityEngine;

public class EnemyRangeHitScan : RangeAttack
{
    #region NonSerializeFields

    private Transform playerTransform;

    #endregion

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    protected override void Update()
    {
        if (!playerTransform) return;

        base.Update();
    }

    protected override bool CantShoot()
    {
        return !(Time.time >= nextTimeToFire);
    }

    protected override bool CheckHit(out RaycastHit2D hitInfo)
    {
        Vector2 enemyPosition = transform.position;
        Vector2 playerDirection = ((Vector2) playerTransform.position - enemyPosition);
        hitInfo = Physics2D.Raycast(enemyPosition, playerDirection, shootingRange);
        return !hitInfo;
    }

    protected override void Shoot()
    {
        if (CheckHit(out var hitInfo)) return;

        var hitParticle = Instantiate(gunHitEffect, hitInfo.point,
            Quaternion.FromToRotation(Vector2.up, hitInfo.normal));
        Destroy(hitParticle, hitEffectDuration);
        if (hitInfo.transform.CompareTag("Player"))
        {
            hitInfo.transform.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}