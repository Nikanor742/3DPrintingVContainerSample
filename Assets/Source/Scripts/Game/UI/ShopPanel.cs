using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Configs;
using Game.Data;
using Game.Gameplay;
using NaughtyAttributes;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ShopPanel : MonoBehaviour
    {
        public Subject<Unit> OnCloseEvent { get; } = new();

        [Header("Buttons")] 
        [SerializeField] private Button _closeButton;

        [SerializeField] private ShopCard _shopCard;

        private List<ShopCard> _shopCards = new();
        private Dictionary<EEquipType, int> _itemsCost = new();
        private RectTransform _rectTransform;

        private bool _isShown;
        private bool _transitionState;
        private Vector2 _closePosition;
        private Vector2 _openPosition;
        private ShopCard _selectedShopCard;

        private GameData _gameData;
        private readonly CompositeDisposable _disposables = new();

        public void Init(ShopSO shopConfig, GameData gameData)
        {
            _rectTransform = GetComponent<RectTransform>();
            
            _closePosition = _rectTransform.anchoredPosition;
            _openPosition = new  Vector3(_rectTransform.anchoredPosition.x - _rectTransform.sizeDelta.x,
                _rectTransform.anchoredPosition.y);
            
            gameObject.SetActive(false);

            _closeButton.onClick.AddListener(() => _ = Close());

            _gameData = gameData;
            _gameData.OnInputActionEvent?
                .Subscribe(action =>
                {
                    if (action == EActionType.Exit)
                        _ = Close();
                })
                .AddTo(_disposables);

            CreateShopCards(shopConfig.shopItems);
            
            foreach (var i in shopConfig.shopItems)
                _itemsCost.Add(i.itemType, i.cost);

            SaveExtension.player.resources[EPlayerResourceType.Coins].OnChangeEvent
                .Subscribe(_ =>
                {
                    SetCardsActiveState();
                    SetSelectedCardState();
                })
                .AddTo(_disposables);
        }

        private void CreateShopCards(EquipShopItem[] shopItems)
        {
            foreach (var s in shopItems)
            {
                var card = Instantiate(_shopCard, _shopCard.transform.parent);
                card.gameObject.SetActive(true);
                card.Init(s);
                card.OnSelectEvent
                    .Subscribe(OnSelectCard)
                    .AddTo(_disposables);
                _shopCards.Add(card);
            }
        }

        private void OnSelectCard(ShopCard card)
        {
            var money = SaveExtension.player.resources[EPlayerResourceType.Coins].Count;
            if (money >= _itemsCost[card.EquipType])
            {
                foreach (var c in _shopCards)
                    c.SetSelectState(false);

                card.SetSelectState(true);

                _gameData.OnShopEquipmentSelectedEvent?.OnNext(card.EquipType);
                _selectedShopCard = card;
            }
        }

        private void SetCardsActiveState()
        {
            foreach (var c in _shopCards)
            {
                var money = SaveExtension.player.resources[EPlayerResourceType.Coins].Count;
                c.SetActiveState(money >= _itemsCost[c.EquipType]);
            }
        }

        private void SetSelectedCardState()
        {
            if (_selectedShopCard != null)
            {
                var money = SaveExtension.player.resources[EPlayerResourceType.Coins];
                
                if(money.Count <= _itemsCost[_selectedShopCard.EquipType])
                    _selectedShopCard.SetSelectState(false);
            }
        }
        
        public async UniTask Show()
        {
            if (!_isShown && !_transitionState)
            {
                SetCardsActiveState();
                
                foreach (var c in _shopCards)
                    c.SetSelectState(false);

                _transitionState = true;
                gameObject.SetActive(true);
                await _rectTransform.DOAnchorPos(_openPosition, 0.2f).ToUniTask();

                _isShown = true;
                _transitionState = false;
            }
        }
        
        private async UniTask Close()
        {
            if (_isShown && !_transitionState)
            {
                OnCloseEvent?.OnNext(Unit.Default);
                _transitionState = true;
                await _rectTransform.DOAnchorPos(_closePosition, 0.2f).ToUniTask();
                gameObject.SetActive(false);

                _isShown = false;
                _transitionState = false;
            }
        }

        private void OnDestroy()
        {
            _closeButton?.onClick.RemoveAllListeners();
            _disposables?.Dispose();
        }
    }
}
