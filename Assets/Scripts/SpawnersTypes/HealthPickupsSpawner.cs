using Pools;
using UnityEngine;

namespace SpawnersTypes
{
    public class HealthPickupsSpawner : PickupsSpawner
    {
        protected override GameObject RandomObject()
        {
            return HealthPickupsPool.Instance.Get().gameObject;
        }
    }
}