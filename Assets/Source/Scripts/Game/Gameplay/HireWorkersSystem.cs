using System;
using System.Collections.Generic;
using System.Linq;
using Game.Configs;
using Game.Data;
using Game.UI;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Gameplay
{
    public sealed class HireWorkersSystem : IDisposable
    {
        private readonly GameData _gameData;
        private readonly GameUIView  _gameUIView;
        private readonly WorkersSO _workersSO;
        
        private List<WorkerData> _workersData;
        private List<WorkerCard> _workerCards;
        
        private readonly CompositeDisposable _disposables = new ();
        public HireWorkersSystem(GameData gameData ,GameUIView gameUIView, WorkersSO workersSO)
        {
            _gameData = gameData;
            _gameUIView = gameUIView;
            _workersSO = workersSO;
            
            _gameUIView.hireWorkersWindow.OnRefreshButtonClick
                .Subscribe(_ => OnRefreshButtonClick())
                .AddTo(_disposables);
            
            SaveExtension.player.resources[EPlayerResourceType.Coins].OnChangeEvent
                .Subscribe(money =>
                {
                    _gameUIView.hireWorkersWindow
                        .SetRefreshTextColor(money < _workersSO.refreshWorkersCost ? Color.red : Color.black);;
                })
                .AddTo(_disposables);

            CreateWorkersPool();
        }

        private void OnRefreshButtonClick()
        {
            var money = SaveExtension.player.resources[EPlayerResourceType.Coins];
            if(money.Count < _workersSO.refreshWorkersCost)
                return;

            CreateWorkersPool();
            
            money.Count-=_workersSO.refreshWorkersCost;
            SaveExtension.SaveData();
        }

        private string GetFreeName()
        {
            while (true)
            {
                var name = _workersSO.manNames[Random.Range(0, _workersSO.manNames.Length)];
                var poolNames = _workersData.Select(n => n.name);
                if(!SaveExtension.player.hiredWorkers.ContainsKey(name) && !poolNames.Contains(name))
                    return name;
            }
        }

        private void CreateWorkersPool()
        {
            _workersData = new List<WorkerData>();
            for (int i = 0; i < _workersSO.workersCount; i++)
            {
                var workerData = new WorkerData(GetFreeName(),
                    _workersSO.workersSpeed, 
                    Random.Range(10, 50), 
                    Random.Range(200, 500),
                    1);
                
                _workersData.Add(workerData);
            }
            _workerCards = _gameUIView.hireWorkersWindow.CreateCards(_workersData);

            for (int i = 0; i < _workerCards.Count; i++)
            {
                var index = i;
                _workerCards[i].OnHireButtonClick
                    .Subscribe(_ => OnHireButtonClick(index))
                    .AddTo(_disposables);
            }
        }

        private void OnHireButtonClick(int index)
        {
            var cardView = _workerCards[index];
            var workerData = _workersData[index];

            var money = SaveExtension.player.resources[EPlayerResourceType.Coins];

            if (money.Count >= workerData.hireCost)
            {
                money.Count-= workerData.hireCost;
                Object.Destroy(cardView.gameObject);
                
                SaveExtension.player.hiredWorkers.TryAdd(workerData.name, workerData);
                SaveExtension.SaveData();
                
                _gameData.OnWorkerHiredEvent?.OnNext(workerData);
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}