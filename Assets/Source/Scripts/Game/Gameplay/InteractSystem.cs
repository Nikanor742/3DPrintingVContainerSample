using System;
using Game.Data;
using Game.Interfaces;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Gameplay
{
    public sealed class InteractSystem: IDisposable
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly Camera _camera;
        
        private MoneyChangeView _moneyChangeViewPrefab;
        
        public InteractSystem(GameData gameData, MoneyChangeView moneyChangeViewPrefab)
        {
            _camera = Camera.main;
            _moneyChangeViewPrefab = moneyChangeViewPrefab;
            
            gameData.OnInputActionEvent
                .Subscribe(action =>
                {
                    if (action == EActionType.RightMouseUp)
                    {
                        var ray = _camera.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out var hit))
                        {
                            if (hit.transform.TryGetComponent<IInteractable>(out var interactable))
                                Interact(interactable);
                        }
                    }
                })
                .AddTo(_disposables);
            
            gameData.OnShopChangeStateEvent
                .Subscribe(state =>
                {
                    foreach (var itemsList in gameData.activeObjects.Values)
                    {
                        foreach (var item in itemsList)
                            item.interactable = !state;
                    }
                })
                .AddTo(_disposables);
        }

        private void Interact(IInteractable interactable)
        {
            var money = SaveExtension.player.resources[EPlayerResourceType.Coins];
            
            switch (interactable.InteractType)
            {
                case EInteractType.PurchaseMaterial:
                {
                    if (money.Count >= 10 && interactable.Interact())
                    {
                        money.Count -= 10;
                        SaveExtension.SaveData();
                        
                        var changeMoneyViewPos = interactable.Transform.position;
                        changeMoneyViewPos.y += 2f;
                        var changeMoneyView = Object.Instantiate(_moneyChangeViewPrefab, changeMoneyViewPos, Quaternion.identity); 
                        changeMoneyView.SetCount(-10);
                    }
                    return;
                }
            }
        }
        
        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}