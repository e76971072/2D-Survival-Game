using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyType;
    public float timeBetweenSpawn;

    void Start()
    {
        Spawn();
    }

    void Spawn ()
    {
            Instantiate(enemyType, gameObject.transform.position, enemyType.transform.rotation, gameObject.transform);
            Invoke("Spawn", timeBetweenSpawn);
    }
}
