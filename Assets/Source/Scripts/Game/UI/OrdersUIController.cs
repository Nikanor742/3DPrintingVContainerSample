using System;
using Game.Configs;
using Game.Data;
using R3;
using UnityEngine;

namespace Game.UI
{
    public sealed class OrdersUIController : IDisposable
    {
        private readonly GameUIView _gameUIView;
        
        private readonly CompositeDisposable _disposables = new();
        
        public OrdersUIController(GameUIView gameUIView, GameData gameData, WindowsSO windowsConfig)
        {
            _gameUIView = gameUIView;
            _gameUIView.currentOrdersWindow.Init(gameData, windowsConfig);
            _gameUIView.availableOrdersWindow.Init(gameData, windowsConfig);
            
            _gameUIView.OnOrdersButtonClick
                .Subscribe(_ => OnOrdersButtonClick())
                .AddTo(_disposables);
            
            _gameUIView.currentOrdersWindow.OnFreeCellButtonClick
                .Subscribe(OnFreeCellButtonClick)
                .AddTo(_disposables);
        }

        private void OnOrdersButtonClick()
        {
            _gameUIView.currentOrdersWindow.Show();
        }

        private void OnFreeCellButtonClick(int cellIndex)
        {
            _gameUIView.availableOrdersWindow.SetSelectedIndex(cellIndex);
            _gameUIView.availableOrdersWindow.Show();
        }
        
        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}