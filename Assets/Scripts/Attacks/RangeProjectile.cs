using Pools;
using UnityEngine;

namespace Attacks
{
    public abstract class RangeProjectile : RangeAttack
    {
        #region SerializeFields

        [SerializeField] private ProjectileLogic projectilePrefab;
        [SerializeField] private float shootForce;

        #endregion

        #region NonSerializeFields

        protected Transform muzzleTransform;

        #endregion

        protected virtual void Awake()
        {
            muzzleTransform = transform;
        }

        protected override void Shoot()
        {
            var shotProjectile = Instantiate(projectilePrefab, muzzleTransform.position, Quaternion.identity);
            shotProjectile.SetOwner(gameObject.layer);

            Vector2 targetDirection = GetTargetDirection();
            var moveForce = targetDirection * shootForce;
            shotProjectile.AddForce(moveForce);
        }

        protected abstract Vector3 GetTargetDirection();
    }
}