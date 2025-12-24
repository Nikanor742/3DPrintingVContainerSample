using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class WorkersWindow : Window
    {
        [SerializeField] private Button _hireWorkersButton;
        
        public readonly Subject<Unit> OnHireWorkersButtonClick = new ();

        private void Awake()
        {
            _hireWorkersButton.onClick.AddListener(() => OnHireWorkersButtonClick.OnNext(Unit.Default));
        }

        private void OnDestroy()
        {
            _hireWorkersButton.onClick?.RemoveAllListeners();
        }
    }
}