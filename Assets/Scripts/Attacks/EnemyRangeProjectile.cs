using Helpers;
using UnityEngine;

namespace Attacks
{
    public class EnemyRangeProjectile : RangeProjectile
    {
        private Transform _targetTransform;

        protected override void Awake()
        {
            base.Awake();
            _targetTransform = GameManager.Instance.player.transform;
        }

        protected override void Update()
        {
            if (!_targetTransform) return;

            base.Update();
        }

        protected override bool CantShoot()
        {
            return !(Time.time >= NextTimeToFire);
        }

        protected override Vector3 GetTargetDirection()
        {
            return (_targetTransform.position - transform.position).normalized;
        }
    }
}