using UnityEngine;
using Zenject;

namespace CustomFactories
{
    public class CustomTransformFactory : IFactory<Object, Vector3, Quaternion, Transform, Transform>
    {
        private readonly DiContainer _diContainer;
        
        public CustomTransformFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public Transform Create(Object param, Vector3 position, Quaternion rotation, Transform parentTransform)
        {
            return _diContainer.InstantiatePrefab(param, position, rotation, parentTransform).transform;
        }
    }

    public class TransformFactory : PlaceholderFactory<Object, Vector3, Quaternion, Transform, Transform>
    {
        
    }
}