using System.Collections;
using Player;
using UnityEngine;

namespace Attacks
{
    public class WeaponRangeHitScan : RangeHitScan
    {
        [SerializeField] private bool useAmmo;
        [SerializeField] private int maxAmmo = 30;
        [SerializeField] private int maxAmmoPerClip = 10;
        [SerializeField] private float reloadTime = 1f;
        [SerializeField] private PlayerInput playerInput;

        private Ammo ammo;

        private void OnValidate()
        {
            maxAmmo = Mathf.Clamp(maxAmmo, 0, 70);
            maxAmmoPerClip = Mathf.Clamp(maxAmmoPerClip, 0, maxAmmo);
        }

        protected override void Awake()
        {
            base.Awake();
            if (useAmmo)
            {
                ammo = new Ammo(maxAmmo, maxAmmoPerClip);
            }

            muzzleTransform = transform.GetChild(0).GetComponent<Transform>();
        }

        protected override void Shoot()
        {
            if (useAmmo)
            {
                if (ammo.CurrentAmmo <= 0 && !ammo.IsReloading)
                {
                    StartCoroutine(ReloadAmmo());
                    return;
                }

                ammo.ReduceCurrentAmmo();
            }

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
            if (useAmmo)
            {
                return !playerInput.canAttack || !(Time.time >= nextTimeToFire) || ammo.IsAmmoEmpty() ||
                       ammo.IsReloading;
            }

            return !playerInput.canAttack || !(Time.time >= nextTimeToFire);
        }

        private void OnDisable()
        {
            bulletLineRenderer.enabled = false;

            if (useAmmo)
            {
                ammo.IsReloading = false;
            }
        }
    }
}