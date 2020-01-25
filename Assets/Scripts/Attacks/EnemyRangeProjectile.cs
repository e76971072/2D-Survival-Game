using Helpers;
using UnityEngine;

namespace Attacks
{
    public class EnemyRangeProjectile : RangeProjectile
    {
        private Transform targetTransform;

        protected override void Awake()
        {
            base.Awake();
            targetTransform = GameManager.Instance.player.transform;
        }

        protected override void Update()
        {
            if (!targetTransform) return;

            base.Update();
        }

        protected override bool CantShoot()
        {
            return !(Time.time >= nextTimeToFire);
        }

        protected override Vector3 GetTargetDirection()
        {
            return (targetTransform.position - transform.position).normalized;
        }
    }
}