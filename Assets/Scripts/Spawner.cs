using System.Collections;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject[] prefabs;
    [SerializeField] protected float timeBetweenSpawn;
    
    protected void Start()
    {
        StartCoroutine(Spawn());
    }

    protected abstract IEnumerator Spawn();

    protected GameObject RandomObject()
    {
        return prefabs[RandomIndex()];
    }

    private int RandomIndex()
    {
        return Random.Range(0, prefabs.Length);
    }
}