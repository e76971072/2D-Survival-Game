using Helpers;
using UnityEngine;
using Zenject;

namespace Attacks
{
    public class EnemyRangeProjectile : RangeProjectile
    {
        private Transform _targetTransform;
        private GameManager _gameManager;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        protected override void Awake()
        {
            base.Awake();
            _targetTransform = _gameManager.player.transform;
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