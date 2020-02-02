using Pools;
using UnityEngine;

namespace Attacks
{
    public abstract class RangeProjectile : RangeAttack
    {
        #region SerializeFields

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
            var shotProjectile = ProjectilePools.Instance.Get();
            shotProjectile.transform.position = muzzleTransform.position;
            shotProjectile.SetOwner(gameObject.layer);
            shotProjectile.gameObject.SetActive(true);

            Vector2 targetDirection = GetTargetDirection();
            var moveForce = targetDirection * shootForce;
            shotProjectile.GetComponent<Rigidbody2D>().AddForce(moveForce, ForceMode2D.Impulse);
        }

        protected virtual Vector3 GetTargetDirection()
        {
            return Vector3.right;
        }
    }
}