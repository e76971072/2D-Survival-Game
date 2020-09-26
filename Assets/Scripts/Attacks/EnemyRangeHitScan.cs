using Helpers;
using UnityEngine;

namespace Attacks
{
    public class EnemyRangeHitScan : RangeHitScan
    {
        private Transform _playerTransform;

        protected override void Awake()
        {
            MuzzleTransform = transform;
            _playerTransform = GameManager.Instance.player.transform;
        }

        protected override void Update()
        {
            if (!_playerTransform) return;

            base.Update();
        }

        protected override bool CantShoot()
        {
            return !(Time.time >= NextTimeToFire);
        }

        protected override bool CheckHit(out RaycastHit2D hitInfo)
        {
            Vector2 position = MuzzleTransform.position;
            var targetDirection = (Vector2) _playerTransform.position - position;
            hitInfo = Physics2D.Raycast(position, targetDirection, shootingRange, possibleHitLayer);
            return !hitInfo;
        }
    }
}