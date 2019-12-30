using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] protected float hitEffectDuration = 0.5f;
    [SerializeField] protected int damage;
    [SerializeField] private float firingRate;
    [SerializeField] protected float shootingRange = Mathf.Infinity;
    [SerializeField] protected GameObject gunHitEffect;

    #endregion

    #region NonSerializeFields

    protected float nextTimeToFire;

    #endregion

    protected virtual void Update()
    {
        if (CantShoot()) return;

        nextTimeToFire = Time.time + 1f / firingRate;
        Shoot();
    }

    protected virtual bool CantShoot()
    {
        return !Input.GetButton("Fire1") || !(Time.time >= nextTimeToFire);
    }

    protected virtual void Shoot()
    {
        if (CheckHit(out var hitInfo)) return;

        var hitParticle = Instantiate(gunHitEffect, hitInfo.point,
            Quaternion.FromToRotation(Vector2.up, hitInfo.normal));
        Destroy(hitParticle, hitEffectDuration);
        if (hitInfo.transform.CompareTag("Enemy"))
        {
            hitInfo.transform.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }

    protected virtual bool CheckHit(out RaycastHit2D hitInfo)
    {
        Transform gunTransform = transform;
        Vector2 playerPosition = gunTransform.parent.position;
        hitInfo = Physics2D.Raycast(playerPosition, gunTransform.right, shootingRange);
        return !hitInfo;
    }
}