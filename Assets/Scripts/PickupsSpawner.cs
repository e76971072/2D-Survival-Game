using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupsSpawner : Spawner
{
    [SerializeField] private LayerMask itemBlockingLayer;

    private Collider2D spawnArea;

    private void Awake()
    {
        spawnArea = GetComponent<Collider2D>();
    }

    protected override IEnumerator Spawn()
    {
        while (true)
        {
            Vector2 spawnPosition;
            Bounds colliderBounds = spawnArea.bounds;
            Collider2D[] results;
            do
            {
                results = new Collider2D[2];
                var randomXPosition = Random.Range(colliderBounds.min.x, colliderBounds.max.x);
                var randomYPosition = Random.Range(colliderBounds.min.y, colliderBounds.max.y);
                spawnPosition = new Vector2(randomXPosition, randomYPosition);
                Physics2D.OverlapBoxNonAlloc(spawnPosition, Vector2.one, 0, results, itemBlockingLayer);
            } while (!spawnArea.OverlapPoint(spawnPosition) || results[0] != null);

            GameObject pickupToSpawn = RandomObject();
            Instantiate(pickupToSpawn, spawnPosition, Quaternion.identity, transform);

            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
}