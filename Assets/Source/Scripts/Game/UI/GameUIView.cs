using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GameUIView : MonoBehaviour
    {
        [Header("Panels")] 
        public ShopPanel shopPanel;
        public ButtonsPanel buttonsPanel;

        [Header("Windows")] 
        public WorkersWindow workersWindow;
        public HireWorkersWindow hireWorkersWindow;
        public CurrentOrdersWindow currentOrdersWindow;
        public AvailableOrdersWindow availableOrdersWindow;

        [Header("Resources")] 
        public TMP_Text moneyText;

        public readonly Subject<Unit> OnShopButtonClick = new ();
        public readonly Subject<Unit> OnWorkersButtonClick = new ();
        public readonly Subject<Unit> OnOrdersButtonClick = new ();
        public readonly Subject<Unit> OnMoneyButtonClick = new ();
        
        [Header("Buttons")] 
        [SerializeField] private Button shopButton;
        [SerializeField] private Button workersButton;
        [SerializeField] private Button ordersButton;
        [SerializeField] private Button addMoneyButton;

        private void Awake()
        {
            shopButton.onClick.AddListener(() => OnShopButtonClick.OnNext(Unit.Default));
            workersButton.onClick.AddListener(() => OnWorkersButtonClick.OnNext(Unit.Default));
            ordersButton.onClick.AddListener(() => OnOrdersButtonClick.OnNext(Unit.Default));
            addMoneyButton.onClick.AddListener(() => OnMoneyButtonClick.OnNext(Unit.Default));
        }

        private void OnDestroy()
        {
            shopButton.onClick?.RemoveAllListeners();
            workersButton.onClick?.RemoveAllListeners();
            ordersButton.onClick?.RemoveAllListeners();
            addMoneyButton.onClick?.RemoveAllListeners();
        }
    }
}
