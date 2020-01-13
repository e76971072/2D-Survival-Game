using UnityEngine;

public class WeaponRangeHitScan : RangeHitScan
{
    private PlayerInput playerInput;
    
    protected override void Awake()
    {
        base.Awake();
        muzzleTransform = transform.GetChild(0).GetComponent<Transform>();
        playerInput = transform.parent.parent.GetComponent<PlayerInput>();
    }
    
    protected override bool CantShoot()
    {
        return !playerInput.canShoot || !(Time.time >= nextTimeToFire);
    }
}