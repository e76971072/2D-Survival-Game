using Pools;
using UnityEngine;

namespace SpawnersTypes
{
    public class AmmoPickupsSpawner : PickupsSpawner
    {
        protected override GameObject RandomObject()
        {
            return AmmoPickupsPool.Instance.Get().gameObject;
        }
    }
}