using System;
using System.Collections.Generic;
using Game.Configs;
using Game.Data;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class HireWorkersWindow : Window
    {
        public readonly Subject<Unit> OnRefreshButtonClick = new ();
        
        [SerializeField] private Button _refreshButton;
        [SerializeField] private TMP_Text _refreshWorkersCostText;
        [SerializeField] private WorkerCard _workerCard;

        private readonly List<WorkerCard> _workerCards = new();

        public override void Init(GameData gameData, WindowsSO windowsConfig)
        {
            base.Init(gameData, windowsConfig);
            
            _refreshButton.onClick.AddListener(() => OnRefreshButtonClick.OnNext(Unit.Default));
        }

        public List<WorkerCard> CreateCards(List<WorkerData> workersData)
        {
            foreach (var w in _workerCards)
            {
                if(w !=null)
                    Destroy(w.gameObject);
            }
            _workerCards.Clear();

            foreach (var d in workersData)
            {
                var card = Instantiate(_workerCard, _workerCard.transform.parent);
                card.Init(d);
                card.gameObject.SetActive(true);
                _workerCards.Add(card);
            }
            
            return _workerCards;
        }

        public void SetRefreshTextColor(Color color) => _refreshWorkersCostText.color = color;

        private void OnDestroy()
        {
            _refreshButton.onClick?.RemoveAllListeners();
        }
    }
}

