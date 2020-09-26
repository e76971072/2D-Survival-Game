using System.Collections;
using UnityEngine;

namespace SpawnersTypes
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class PickupsSpawner : Spawner
    {
        [SerializeField] private LayerMask itemBlockingLayer;

        private Collider2D _spawnArea;

        private void Awake()
        {
            _spawnArea = GetComponent<Collider2D>();
        }

        protected override IEnumerator Spawn()
        {
            while (true)
            {
                Vector2 spawnPosition;
                var colliderBounds = _spawnArea.bounds;
                Collider2D[] results;
                do
                {
                    results = new Collider2D[2];
                    var randomXPosition = Random.Range(colliderBounds.min.x, colliderBounds.max.x);
                    var randomYPosition = Random.Range(colliderBounds.min.y, colliderBounds.max.y);
                    spawnPosition = new Vector2(randomXPosition, randomYPosition);
                    Physics2D.OverlapBoxNonAlloc(spawnPosition, Vector2.one, 0, results, itemBlockingLayer);
                } while (!_spawnArea.OverlapPoint(spawnPosition) || results[0] != null);

                var pickupToSpawn = RandomObject();
                pickupToSpawn.transform.position = spawnPosition;
                pickupToSpawn.SetActive(true);

                yield return new WaitForSeconds(timeBetweenSpawn);
            }
        }
    }
}