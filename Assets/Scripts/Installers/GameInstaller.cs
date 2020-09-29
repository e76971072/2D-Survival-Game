using CustomFactories;
using Data;
using Helpers;
using Props;
using Signals;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private HealthBar _healthBar;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<GameLostSignal>();

            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
            Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle();

            Container.BindInterfacesAndSelfTo<Score>().AsSingle();
            Container.Bind<HitCombo>().AsSingle();

            Container.BindMemoryPool<HealthBar, HealthBar.Pool>().WithInitialSize(10)
                .FromComponentInNewPrefab(_healthBar)
                .WithGameObjectName("HealthBar");
            Container.BindFactory<Object, Transform, PlaceholderTransformFactory>()
                .FromFactory<CustomTransformFactory>();
        }
    }
}