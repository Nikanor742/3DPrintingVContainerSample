using System;
using Game.Configs;
using Game.Data;
using R3;
using UnityEngine;

namespace Game.UI
{
    public sealed class WorkersUIController : IDisposable
    {
        private readonly GameUIView _gameUIView;
        private readonly CompositeDisposable _disposables = new();

        public WorkersUIController(GameUIView gameUIView, GameData gameData, WindowsSO windowsConfig)
        {
            _gameUIView =  gameUIView;
            _gameUIView.workersWindow.Init(gameData, windowsConfig);
            _gameUIView.hireWorkersWindow.Init(gameData, windowsConfig);

            _gameUIView.OnWorkersButtonClick
                .Subscribe(_ => OnWorkersButtonClick())
                .AddTo(_disposables);
            
            _gameUIView.workersWindow.OnHireWorkersButtonClick
                .Subscribe(_ => OnHireWorkersButtonClick())
                .AddTo(_disposables);
        }
        
        private void OnWorkersButtonClick()
        {
            _gameUIView.workersWindow.Show();
        }

        private void OnHireWorkersButtonClick()
        {
            _gameUIView.hireWorkersWindow.Show();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}