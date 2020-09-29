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

        protected Transform MuzzleTransform;

        #endregion

        protected virtual void Awake()
        {
            MuzzleTransform = transform;
        }

        protected override void Shoot()
        {
            var shotProjectile = Instantiate(projectilePrefab, MuzzleTransform.position, Quaternion.identity);
            shotProjectile.SetOwner(gameObject.layer);

            Vector2 targetDirection = GetTargetDirection();
            var moveForce = targetDirection * shootForce;
            shotProjectile.AddForce(moveForce);
        }

        protected abstract Vector3 GetTargetDirection();
    }
}