using System.Collections;
using CustomFactories;
using UnityEngine;
using Zenject;

namespace SpawnersTypes
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private GameObject[] enemyList;

        private PlaceholderTransformFactory _transformFactory;

        [Inject]
        public void Construct(PlaceholderTransformFactory transformFactory)
        {
            _transformFactory = transformFactory;
        }
        
        protected override IEnumerator Spawn()
        {
            while (true)
            {
                var spawnerTransform = transform;
                var enemyToSpawn = RandomObject();
                
                var newEnemyTransform = _transformFactory.Create(enemyToSpawn);
                newEnemyTransform.position = spawnerTransform.position;
                newEnemyTransform.SetParent(spawnerTransform);
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