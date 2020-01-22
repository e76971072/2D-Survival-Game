using UnityEngine;

public abstract class RangeAttack : MonoBehaviour
{
    #region SerializeFields

    [SerializeField] protected float firingRate;

    #endregion

    #region NonSerializeFields

    protected float nextTimeToFire;

    #endregion

    protected virtual void Update()
    {
        if (CantShoot()) return;

        nextTimeToFire = Time.time + 1f / firingRate;
        Shoot();
    }

    protected virtual bool CantShoot()
    {
        return !Input.GetButton("Fire1") || !(Time.time >= nextTimeToFire);
    }

    protected abstract void Shoot();
}