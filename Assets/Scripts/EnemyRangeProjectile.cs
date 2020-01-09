using UnityEngine;

public class EnemyRangeProjectile : RangeProjectile
{
    #region NonSerializeFields

    private Transform targetTransform;

    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        targetTransform = GameObject.FindWithTag("Player").transform;
    }
    
    protected override void Update()
    {
        if (!targetTransform) return;

        base.Update();
    }

    protected override bool CantShoot()
    {
        return !(Time.time >= nextTimeToFire);
    }

    protected override Vector3 GetTargetDirection()
    {
        return (targetTransform.position - transform.position).normalized;
    }
}