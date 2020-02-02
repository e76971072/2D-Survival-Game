using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
    {
        public static GenericObjectPool<T> Instance { get; private set; }
        
        [SerializeField] private T prefab;

        private readonly Queue<T> objects = new Queue<T>();
        
        private void Awake()
        {
            Instance = this;
        }

        public T Get()
        {
            if (objects.Count == 0)
            {
                AddObjects(1);
            }

            return objects.Dequeue();
        }

        public void ReturnToPool(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            objects.Enqueue(objectToReturn);
        }

        private void AddObjects(int i)
        {
            var newObject = Instantiate(prefab);
            newObject.gameObject.SetActive(false);
            objects.Enqueue(newObject);
        }
    }
}