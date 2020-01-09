using UnityEngine;

public class WeaponRangeHitScan : RangeHitScan
{
    private void Awake()
    {
        muzzleTransform = transform.GetChild(0).GetComponent<Transform>();
    }
}