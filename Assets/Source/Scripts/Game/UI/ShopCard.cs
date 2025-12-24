using Game.Configs;
using Game.Data;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ShopCard : MonoBehaviour
    {
        public EEquipType EquipType => _equipType;
        public Subject<ShopCard> OnSelectEvent { get; } = new();

        [SerializeField] private GameObject _activeBorder;
        [SerializeField] private Image _cardImage;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private Button _button;
        [SerializeField] private Color _inactiveColor;

        private EEquipType _equipType;
        private bool _selected;

        public void Init(EquipShopItem itemData)
        {
            _equipType = itemData.itemType;
            _cardImage.sprite = itemData.shopIcon;
            _costText.text = $"{itemData.cost} {GlobalConstants.moneyTag}";

            _button.onClick.AddListener(OnSelect);
        }

        public void SetSelectState(bool state)
        {
            _selected = state;
            _activeBorder.SetActive(state);
        }

        public void SetActiveState(bool state)
        {
            _cardImage.color = state ? Color.white : _inactiveColor;
        }

        private void OnSelect()
        {
            if (!_selected)
                OnSelectEvent?.OnNext(this);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
