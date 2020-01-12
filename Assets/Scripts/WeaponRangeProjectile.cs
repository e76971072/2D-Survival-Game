using UnityEngine;

public class WeaponRangeProjectile : RangeProjectile
{
    private PlayerInput playerInput;
    
    protected override void Awake()
    {
        muzzleTransform = transform.GetChild(0).GetComponent<Transform>();
        playerInput = transform.parent.parent.GetComponent<PlayerInput>();
    }

    protected override bool CantShoot()
    {
        return !playerInput.canShoot || !(Time.time >= nextTimeToFire);
    }

    protected override Vector3 GetTargetDirection()
    {
        return (GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
    }
}