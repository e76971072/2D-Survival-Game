using UnityEngine;
using Zenject;

namespace CustomFactories
{
    public class CustomTransformFactory : IFactory<Object, Transform>
    {
        private readonly DiContainer _diContainer;
        
        public CustomTransformFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public Transform Create(Object param)
        {
            return _diContainer.InstantiatePrefab(param).transform;
        }
    }

    public class PlaceholderTransformFactory : PlaceholderFactory<Object, Transform>
    {
        
    }
}