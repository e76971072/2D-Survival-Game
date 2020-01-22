using System.Collections;
using UnityEngine;

public class WeaponRangeHitScan : RangeHitScan
{
    [SerializeField] public Ammo ammo;
    [SerializeField] private float reloadTime = 1f;

    private PlayerInput playerInput;

    protected override void Awake()
    {
        base.Awake();
        muzzleTransform = transform.GetChild(0).GetComponent<Transform>();
        playerInput = transform.parent.parent.GetComponent<PlayerInput>();

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
        return !playerInput.canShoot || !(Time.time >= nextTimeToFire) || ammo.IsAmmoEmpty();
    }
    
    private void OnDisable()
    {
        bulletLineRenderer.enabled = false;
        ammo.StopReloading();
    }
}