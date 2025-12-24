using Game.Configs;
using Game.Data;
using Game.Gameplay;
using Game.Interfaces;
using Game.UI;
using Source.Scripts.Game.Gameplay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.DI
{
    public sealed class GameLTS : LifetimeScope
    {
        [Header("Prefabs")] 
        [SerializeField] private GameUIView _gameViewPrefab;
        [SerializeField] private CameraView _cameraViewPrefab;
        [SerializeField] private GridSystem _gridSystemPrefab;
        [SerializeField] private MoneyChangeView _moneyChangeViewPrefab;
        [SerializeField] private NavmeshBaker _navmeshBakerPrefab;

        [Header("Configs")] 
        [SerializeField] private ShopSO _shopConfig;
        [SerializeField] private ActionsSO _actionsConfig;
        [SerializeField] private CameraSettingsSO _cameraSettings;
        [SerializeField] private WindowsSO _windowsConfig;
        [SerializeField] private WorkersSO _workersSO;
        
        protected override void Configure(IContainerBuilder builder)
        {
            SaveExtension.Override();
            
            builder.RegisterInstance(_shopConfig);
            builder.RegisterInstance(_actionsConfig);
            builder.RegisterInstance(_cameraSettings);
            builder.RegisterInstance(_windowsConfig);
            builder.RegisterInstance(_workersSO);
            builder.RegisterInstance(_moneyChangeViewPrefab);

            builder.Register<GameData>(Lifetime.Scoped);

            builder.RegisterComponentInNewPrefab(_gameViewPrefab, Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(_cameraViewPrefab, Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(_gridSystemPrefab, Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(_navmeshBakerPrefab, Lifetime.Scoped);
            
            builder.Register<InputSystem>(Lifetime.Scoped);
            builder.Register<MainUIController>(Lifetime.Scoped);
            builder.Register<WorkersUIController>(Lifetime.Scoped);
            builder.Register<OrdersUIController>(Lifetime.Scoped);
            builder.Register<CameraController>(Lifetime.Scoped);
            builder.Register<InteractSystem>(Lifetime.Scoped);
            builder.Register<HireWorkersSystem>(Lifetime.Scoped);
            builder.Register<WorkersSystem>(Lifetime.Scoped);
            builder.Register<OrdersSystem>(Lifetime.Scoped);
            builder.Register<PurchasedEquipmentCreator>(Lifetime.Scoped);
            
            builder.Register<IWorkerFactory, WorkerFactory>(Lifetime.Scoped);
            builder.Register<IEquipmentFactory, EquipmentFactory>(Lifetime.Scoped);
            
            builder.RegisterEntryPoint<GameInitializer>();
        }
    }
}