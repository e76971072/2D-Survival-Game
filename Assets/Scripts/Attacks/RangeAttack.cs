using UnityEngine;

namespace Attacks
{
    public abstract class RangeAttack : MonoBehaviour
    {
        [SerializeField] protected float firingRate;

        protected float nextTimeToFire;

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
}