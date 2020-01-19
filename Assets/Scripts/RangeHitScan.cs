using System.Collections;
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
    protected LineRenderer bulletLineRenderer;

    #endregion

    protected virtual void Awake()
    {
        bulletLineRenderer = GetComponent<LineRenderer>();
        bulletLineRenderer.enabled = false;
    }

    protected override void Shoot()
    {
        if (CheckHit(out RaycastHit2D hitInfo)) return;

        StopAllCoroutines();
        StartCoroutine(DrawBulletTrail(hitInfo));
        var hitParticle = Instantiate(gunHitEffect, hitInfo.point,
            Quaternion.FromToRotation(Vector2.up, hitInfo.normal));
        Destroy(hitParticle, hitEffectDuration);
        if (hitInfo.transform.CompareTag(targetTag))
        {
            hitInfo.transform.GetComponent<IHealth>().ModifyHealth(-damage);
        }
    }

    private IEnumerator DrawBulletTrail(RaycastHit2D hitInfo)
    {
        bulletLineRenderer.SetPosition(0, muzzleTransform.position);
        bulletLineRenderer.SetPosition(1, hitInfo.point);
        bulletLineRenderer.enabled = true;
        yield return new WaitForSecondsRealtime(0.1f);
        bulletLineRenderer.enabled = false;
    }

    protected virtual bool CheckHit(out RaycastHit2D hitInfo)
    {
        hitInfo = Physics2D.Raycast(muzzleTransform.position, muzzleTransform.right, shootingRange, targetLayer);
        return !hitInfo;
    }
}