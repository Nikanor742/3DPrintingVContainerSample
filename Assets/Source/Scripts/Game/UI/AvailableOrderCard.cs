using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class AvailableOrderCard : MonoBehaviour
    {
        public Subject<Unit> OnSelect = new ();
        
        [SerializeField] private Image _characterImage;
        [SerializeField] private Image _orderImage;
        
        [SerializeField] private TMP_Text _characterText;
        [SerializeField] private TMP_Text _orderCountText;
        [SerializeField] private TMP_Text _rewardText;
        
        [SerializeField] private Button _selectButton;

        public void SetData()
        {
            
        }
        
        private void Awake()
        {
            _selectButton.onClick.AddListener(()=> OnSelect.OnNext(Unit.Default));
        }

        private void OnDestroy()
        {
            _selectButton.onClick.RemoveAllListeners();
        }
    }
}