using Helpers;
using UnityEngine;
using Zenject;

namespace Attacks
{
    public class WeaponRangeProjectile : RangeProjectile
    {
        private GameManager _gameManager;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        protected override void Awake()
        {
            MuzzleTransform = transform.GetChild(0).GetComponent<Transform>();
        }

        protected override bool CantShoot()
        {
            return !_gameManager.playerInput.canShoot || !(Time.time >= NextTimeToFire);
        }

        protected override Vector3 GetTargetDirection()
        {
            return (_gameManager.mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position)
                .normalized;
        }
    }
}