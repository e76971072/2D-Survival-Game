using UnityEngine;

public class WeaponRangeProjectile : RangeProjectile
{
    protected override void Awake()
    {
        muzzleTransform = transform.GetChild(0).GetComponent<Transform>();
    }

    protected override bool CantShoot()
    {
        return !Input.GetButton("Fire1") || !(Time.time >= nextTimeToFire);
    }

    protected override Vector3 GetTargetDirection()
    {
        return (GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
    }
}