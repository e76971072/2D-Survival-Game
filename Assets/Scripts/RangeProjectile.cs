using UnityEngine;

public abstract class RangeProjectile : RangeAttack
{
    #region SerializeFields

    [SerializeField] protected ProjectileLogic projectilePrefab;
    [SerializeField] private float shootForce;

    #endregion

    #region NonSerializeFields

    protected Transform muzzleTransform;

    #endregion

    protected virtual void Awake()
    {
        muzzleTransform = transform;
    }

    protected override void Shoot()
    {
        Vector2 targetDirection = GetTargetDirection();

        ProjectileLogic shotProjectile = Instantiate(projectilePrefab, muzzleTransform.position,
            Quaternion.FromToRotation(Vector2.up, targetDirection));
        shotProjectile.SetOwner(gameObject.layer);

        Destroy(shotProjectile.gameObject, 5f);
        Vector2 moveForce = targetDirection * shootForce;
        shotProjectile.GetComponent<Rigidbody2D>().AddForce(moveForce, ForceMode2D.Impulse);
    }

    protected virtual Vector3 GetTargetDirection()
    {
        return Vector3.right;
    }
}