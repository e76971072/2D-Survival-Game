using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner
{
    protected override IEnumerator Spawn()
    {
        while (true)
        {
            var spawnerTransform = transform;
            var enemyToSpawn = RandomObject();
            Instantiate(enemyToSpawn, spawnerTransform.position, enemyToSpawn.transform.rotation, spawnerTransform);
            yield return new WaitForSecondsRealtime(timeBetweenSpawn);
        }
    }
}