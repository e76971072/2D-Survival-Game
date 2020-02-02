using System.Collections;
using UnityEngine;

namespace SpawnersTypes
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private GameObject[] enemyList;

        protected override IEnumerator Spawn()
        {
            while (true)
            {
                var spawnerTransform = transform;
                var enemyToSpawn = RandomObject();
                Instantiate(enemyToSpawn, spawnerTransform.position, enemyToSpawn.transform.rotation, spawnerTransform);
                yield return new WaitForSeconds(timeBetweenSpawn);
            }
        }

        protected override GameObject RandomObject()
        {
            return enemyList[RandomIndex()];
        }

        private int RandomIndex()
        {
            return Random.Range(0, enemyList.Length);
        }
    }
}