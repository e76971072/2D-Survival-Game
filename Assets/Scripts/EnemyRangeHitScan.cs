using UnityEngine;

public class EnemyRangeHitScan : RangeHitScan
{
    #region NonSerializeFields

    private Transform playerTransform;

    #endregion

    private void Awake()
    {
        muzzleTransform = transform;
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    protected override void Update()
    {
        if (!playerTransform) return;

        base.Update();
    }

    protected override bool CantShoot()
    {
        return !(Time.time >= nextTimeToFire);
    }

    protected override bool CheckHit(out RaycastHit2D hitInfo)
    {
        Vector2 position = muzzleTransform.position;
        Vector2 targetDirection = ((Vector2) playerTransform.position - position);
        hitInfo = Physics2D.Raycast(position, targetDirection, shootingRange, targetLayer);
        return !hitInfo;
    }
}