using System.Collections;
using Helpers;
using UnityEngine;

namespace Attacks
{
    public class WeaponRangeHitScan : RangeHitScan
    {
        [SerializeField] private Ammo ammo;
        [SerializeField] private float reloadTime = 1f;

        protected override void Awake()
        {
            base.Awake();
            muzzleTransform = transform.GetChild(0).GetComponent<Transform>();

            ammo = new Ammo();
            ammo.UpdateAmmoUi();
        }

        protected override void Shoot()
        {
            if (ammo.GetCurrentAmmo <= 0)
            {
                if (ammo.IsReloading) return;

                StartCoroutine(ReloadAmmo());
                return;
            }

            ammo.ReduceCurrentAmmo();
            base.Shoot();
        }

        private IEnumerator ReloadAmmo()
        {
            ammo.StartReloading();
            yield return new WaitForSeconds(reloadTime);
            ammo.Reload();
        }

        protected override bool CantShoot()
        {
            return !GameManager.Instance.playerInput.canAttack || !(Time.time >= nextTimeToFire) || ammo.IsAmmoEmpty();
        }

        private void OnDisable()
        {
            bulletLineRenderer.enabled = false;
            ammo.StopReloading();
        }
    }
}