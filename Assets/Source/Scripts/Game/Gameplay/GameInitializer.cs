using Game.Configs;
using Game.Data;
using Game.Gameplay;
using Game.UI;
using UnityEngine;
using VContainer.Unity;

namespace Game.DI
{
    public sealed class GameInitializer : IInitializable
    {
        private readonly GridSystem _gridSystem;
        private readonly NavmeshBaker _navmeshBaker;
        private readonly GameData _gameData;
        private readonly ShopSO _shopConfig;
        private readonly MoneyChangeView _moneyChangeViewPrefab;
        
        private readonly InputSystem _inputSystem;
        private readonly MainUIController _mainUIController;
        private readonly WorkersUIController _workersUIController;
        private readonly OrdersUIController _ordersUIController;
        private readonly CameraController _cameraController;
        private readonly InteractSystem _interactSystem;
        private readonly HireWorkersSystem _hireWorkersSystem;
        private readonly WorkersSystem _workersSystem;
        private readonly OrdersSystem _ordersSystem;
        private readonly PurchasedEquipmentCreator _purchasedEquipmentCreator;

        public GameInitializer(
            GridSystem gridSystem,
            NavmeshBaker navmeshBaker,
            GameData gameData,
            ShopSO shopConfig,
            MoneyChangeView moneyChangeViewPrefab,
            InputSystem inputSystem,
            MainUIController mainUIController,
            WorkersUIController workersUIController,
            OrdersUIController ordersUIController,
            CameraController cameraController,
            InteractSystem interactSystem,
            HireWorkersSystem hireWorkersSystem,
            WorkersSystem workersSystem,
            OrdersSystem ordersSystem,
            PurchasedEquipmentCreator purchasedEquipmentCreator)
        {
            _gridSystem = gridSystem;
            _navmeshBaker = navmeshBaker;
            _gameData = gameData;
            _shopConfig = shopConfig;
            _moneyChangeViewPrefab = moneyChangeViewPrefab;
            _inputSystem = inputSystem;
            _mainUIController = mainUIController;
            _workersUIController = workersUIController;
            _ordersUIController = ordersUIController;
            _cameraController = cameraController;
            _interactSystem = interactSystem;
            _hireWorkersSystem = hireWorkersSystem;
            _workersSystem = workersSystem;
            _ordersSystem = ordersSystem;
            _purchasedEquipmentCreator = purchasedEquipmentCreator;
        }
        
        public void Initialize()
        {
            Application.targetFrameRate = 60;

            new GameObject("[DataDrawer]").AddComponent<DataDrawer>();
            
            _gridSystem.Initialize();
            _navmeshBaker.BuildNavMesh();
        }
    }
}