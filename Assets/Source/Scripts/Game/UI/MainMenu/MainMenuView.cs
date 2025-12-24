using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        public Subject<Unit> OnStartClicked { get; } = new();
        public Subject<Unit> OnSettingsClicked { get; } = new();
        public Subject<Unit> OnExitClicked { get; } = new();

        private void Awake()
        {
            _startGameButton.onClick.AddListener(() => OnStartClicked.OnNext(Unit.Default));
            _settingsButton.onClick.AddListener(() => OnSettingsClicked.OnNext(Unit.Default));
            _exitButton.onClick.AddListener(() => OnExitClicked.OnNext(Unit.Default));
        }

        private void OnDestroy()
        {
            OnStartClicked?.Dispose();
            OnSettingsClicked?.Dispose();
            OnExitClicked?.Dispose();
        }
    }
}
