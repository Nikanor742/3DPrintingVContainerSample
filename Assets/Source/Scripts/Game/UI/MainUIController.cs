using System;
using Cysharp.Threading.Tasks;
using Game.Configs;
using Game.Data;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.UI
{
    public class MainUIController : IDisposable
    {
        private readonly GameUIView _gameUI;
        private readonly GameData _gameData;

        private readonly CompositeDisposable _disposables = new();

        public MainUIController(GameUIView gameUI, ShopSO shopConfig, GameData gameData)
        {
            _gameUI = gameUI;
            _gameData = gameData;

            _gameUI.shopPanel.Init(shopConfig, gameData);
            _gameUI.buttonsPanel.Init();

            _gameUI.OnShopButtonClick
                .Subscribe(_ => OnShopButtonClick())
                .AddTo(_disposables);
            
            _gameUI.OnMoneyButtonClick
                .Subscribe(_ =>
                {
                    SaveExtension.player.resources[EPlayerResourceType.Coins].Count += 1000;
                    SaveExtension.SaveData();
                })
                .AddTo(_disposables);
            
            _gameUI.shopPanel.OnCloseEvent
                .Subscribe(_ => OnCloseShop())
                .AddTo(_disposables);
            
            SaveExtension.player.resources[EPlayerResourceType.Coins].OnChangeEvent
                .Subscribe(count => _gameUI.moneyText.text = $"{count} {GlobalConstants.moneyTag}")
                .AddTo(_disposables);
        }

        private void OnShopButtonClick()
        {
            _gameUI.shopPanel.Show().Forget();
            _gameUI.buttonsPanel.Close().Forget();

            _gameData.OnShopChangeStateEvent?.OnNext(true);
        }

        private void OnCloseShop()
        {
            _gameUI.buttonsPanel.Show().Forget();
            _gameData.OnShopChangeStateEvent?.OnNext(false);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
