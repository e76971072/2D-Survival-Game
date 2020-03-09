using System.Collections;
using UnityEngine;

namespace SpawnersTypes
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] protected float timeBetweenSpawn;

        protected void Start()
        {
            StartCoroutine(Spawn());
        }

        protected abstract IEnumerator Spawn();

        protected abstract GameObject RandomObject();
    }
}