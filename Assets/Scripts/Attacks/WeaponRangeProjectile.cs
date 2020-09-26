using Helpers;
using UnityEngine;

namespace Attacks
{
    public class WeaponRangeProjectile : RangeProjectile
    {
        protected override void Awake()
        {
            MuzzleTransform = transform.GetChild(0).GetComponent<Transform>();
        }

        protected override bool CantShoot()
        {
            return !GameManager.Instance.playerInput.canShoot || !(Time.time >= NextTimeToFire);
        }

        protected override Vector3 GetTargetDirection()
        {
            return (GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position)
                .normalized;
        }
    }
}