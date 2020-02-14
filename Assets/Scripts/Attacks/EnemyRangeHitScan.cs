using Helpers;
using UnityEngine;

namespace Attacks
{
    public class EnemyRangeHitScan : RangeHitScan
    {
        private Transform playerTransform;

        protected override void Awake()
        {
            muzzleTransform = transform;
            playerTransform = GameManager.Instance.player.transform;
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
            Vector2 position = muzzleTransform.position;
            var targetDirection = (Vector2) playerTransform.position - position;
            hitInfo = Physics2D.Raycast(position, targetDirection, shootingRange, possibleHitLayer);
            return !hitInfo;
        }
    }
}