using UnityEngine;

public class RangeProjectile : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] private float firingRate;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] private float shootForce;

    #endregion

    #region NonSerializeFields

    private float nextTimeToFire;
    private Transform targetTransform;

    #endregion

    protected void Awake()
    {
        targetTransform = GetTargetTransform();
    }

    protected virtual void Update()
    {
        if (CantShoot()) return;

        nextTimeToFire = Time.time + 1f / firingRate;
        Shoot();
    }

    protected virtual bool CantShoot()
    {
        return !(Time.time >= nextTimeToFire);
    }

    protected virtual void Shoot()
    {
        Vector2 targetDirection = GetTargetDirection();

        GameObject shotProjectile = Instantiate(projectilePrefab, transform.position,
            Quaternion.FromToRotation(Vector2.up, targetDirection));

        Destroy(shotProjectile, 5f);
        Vector2 moveForce = targetDirection * shootForce;
        shotProjectile.GetComponent<Rigidbody2D>().AddForce(moveForce);
    }

    protected virtual Vector3 GetTargetDirection()
    {
        return (targetTransform.position - transform.position).normalized;
    }

    protected virtual Transform GetTargetTransform()
    {
        return GameObject.FindWithTag("Player").transform;
    }
}