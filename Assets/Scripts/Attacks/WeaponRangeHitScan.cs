using System;
using System.Collections;
using Helpers;
using UnityEngine;

namespace Attacks
{
    [RequireComponent(typeof(Ammo))]
    public class WeaponRangeHitScan : RangeHitScan
    {
        [SerializeField] private float reloadTime = 1f;

        private Ammo ammo;

        protected override void Awake()
        {
            base.Awake();
            ammo = GetComponent<Ammo>();
            muzzleTransform = transform.GetChild(0).GetComponent<Transform>();
        }

        protected override void Shoot()
        {
            if (ammo.CurrentAmmo <= 0 && !ammo.IsReloading)
            {
                StartCoroutine(ReloadAmmo());
                return;
            }

            ammo.ReduceCurrentAmmo();
            base.Shoot();
        }

        private IEnumerator ReloadAmmo()
        {
            ammo.IsReloading = true;
            yield return new WaitForSeconds(reloadTime);
            ammo.Reload();
        }

        protected override bool CantShoot()
        {
            return !GameManager.Instance.playerInput.canShoot || !(Time.time >= nextTimeToFire) || ammo.IsAmmoEmpty() || ammo.IsReloading;
        }

        private void OnDisable()
        {
            bulletLineRenderer.enabled = false;
            ammo.IsReloading = false;
        }
    }
}