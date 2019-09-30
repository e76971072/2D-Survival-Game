using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyType;
    [SerializeField] private float timeBetweenSpawn;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            var spawnerPosition = transform;
            var enemyToSpawn = RandomEnemy();
            Instantiate(enemyToSpawn, spawnerPosition.position, enemyToSpawn.transform.rotation, spawnerPosition);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    private GameObject RandomEnemy()
    {
        return enemyType[RandomEnemyIndex()];
    }

    private int RandomEnemyIndex()
    {
        return Random.Range(0, enemyType.Length);
    }
}