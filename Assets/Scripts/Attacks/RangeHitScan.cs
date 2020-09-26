using System.Collections;
using Cinemachine;
using Interfaces;
using UnityEngine;

namespace Attacks
{
    public abstract class RangeHitScan : RangeAttack
    {
        #region SerializeFields

        [SerializeField] protected int damage;
        [SerializeField] protected float shootingRange = Mathf.Infinity;
        [SerializeField] protected LayerMask possibleHitLayer;
        [SerializeField] [TagField] protected string targetTag;
        [SerializeField] protected GameObject gunHitEffect;
        [SerializeField] protected float hitEffectDuration = 0.5f;

        #endregion

        #region NonSerializeFields

        protected Transform MuzzleTransform;
        protected LineRenderer BulletLineRenderer;

        #endregion

        protected virtual void Awake()
        {
            BulletLineRenderer = GetComponent<LineRenderer>();
            BulletLineRenderer.enabled = false;
        }

        protected override void Shoot()
        {
            if (CheckHit(out var hitInfo)) return;

            StopAllCoroutines();
            StartCoroutine(DrawBulletTrail(hitInfo));
            var hitParticle = Instantiate(gunHitEffect, hitInfo.point,
                Quaternion.FromToRotation(Vector2.up, hitInfo.normal));
            Destroy(hitParticle, hitEffectDuration);
            if (hitInfo.transform.CompareTag(targetTag))
                hitInfo.transform.GetComponent<IDamageable>().TakeDamage(damage);
        }

        private IEnumerator DrawBulletTrail(RaycastHit2D hitInfo)
        {
            BulletLineRenderer.SetPosition(0, MuzzleTransform.position);
            BulletLineRenderer.SetPosition(1, hitInfo.point);
            BulletLineRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
            BulletLineRenderer.enabled = false;
        }

        protected virtual bool CheckHit(out RaycastHit2D hitInfo)
        {
            hitInfo = Physics2D.Raycast(MuzzleTransform.position, MuzzleTransform.right, shootingRange,
                possibleHitLayer);
            return !hitInfo;
        }
    }
}