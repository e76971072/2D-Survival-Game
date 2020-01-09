using Cinemachine;
using UnityEngine;

public abstract class RangeHitScan : RangeAttack
{
    #region SerializeFields

    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] [TagField] protected string targetTag;
    [SerializeField] protected float hitEffectDuration = 0.5f;
    [SerializeField] protected int damage;
    [SerializeField] protected float shootingRange = Mathf.Infinity;
    [SerializeField] protected GameObject gunHitEffect;

    #endregion

    #region NonSerializeFields

    protected Transform muzzleTransform;

    #endregion

    protected override bool CantShoot()
    {
        return !Input.GetButton("Fire1") || !(Time.time >= nextTimeToFire);
    }

    protected override void Shoot()
    {
        if (CheckHit(out RaycastHit2D hitInfo)) return;

        var hitParticle = Instantiate(gunHitEffect, hitInfo.point,
            Quaternion.FromToRotation(Vector2.up, hitInfo.normal));
        Destroy(hitParticle, hitEffectDuration);
        if (hitInfo.transform.CompareTag(targetTag))
        {
            hitInfo.transform.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
    
    protected virtual bool CheckHit(out RaycastHit2D hitInfo)
    {
        hitInfo = Physics2D.Raycast(muzzleTransform.position, muzzleTransform.right, shootingRange, targetLayer);
        return !hitInfo;
    }
}