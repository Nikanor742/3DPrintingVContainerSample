using Game.Data;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class WorkerCard : MonoBehaviour
    {
        public readonly Subject<Unit> OnHireButtonClick = new ();
        
        [SerializeField] private Image _workerImage;
        [SerializeField] private TMP_Text _salaryText;
        [SerializeField] private TMP_Text _hireCostText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Button _hireButton;

        private readonly CompositeDisposable _disposable = new();

        private int _salary;
        private int _hireCost;

        public void Init(WorkerData data)
        {
            _hireButton.onClick.AddListener(() => OnHireButtonClick.OnNext(Unit.Default));
            
            //_workerImage.sprite = data.image;
            _salaryText.text = $"salary:{data.salary} {GlobalConstants.moneyTag}";
            _hireCostText.text = $"{data.hireCost} {GlobalConstants.moneyTag}";
            _nameText.text = data.name;
            _levelText.text = data.level.ToString();
            
            _salary = data.salary;
            _hireCost = data.hireCost;
            
            SaveExtension.player.resources[EPlayerResourceType.Coins].OnChangeEvent
                .Subscribe(RefreshCost)
                .AddTo(_disposable);
        }

        private void RefreshCost(int money) => _hireCostText.color = money < _hireCost ? Color.red : Color.black;

        private void OnDestroy()
        {
            _hireButton.onClick?.RemoveAllListeners();
            _disposable?.Dispose();
        }
    }
}

