using UnityEngine;

namespace Attacks
{
    public abstract class RangeAttack : MonoBehaviour
    {
        [SerializeField] protected float firingRate;

        protected float NextTimeToFire;

        protected virtual void Update()
        {
            if (CantShoot()) return;

            NextTimeToFire = Time.time + 1f / firingRate;
            Shoot();
        }

        protected abstract bool CantShoot();

        protected abstract void Shoot();
    }
}