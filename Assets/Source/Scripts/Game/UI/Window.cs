using DG.Tweening;
using Game.Configs;
using Game.Data;
using UnityEngine;
using UnityEngine.UI;
using R3;

namespace Game.UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] protected Button _shadeButton;
        [SerializeField] protected Button _closeButton;
        
        protected WindowsSO _windowsConfig;
        protected GameData _gameData;
        protected RectTransform _rectTransform;

        public virtual void Init(GameData gameData, WindowsSO windowsConfig)
        {
            _windowsConfig = windowsConfig;
            _gameData = gameData;
            
            _rectTransform = GetComponent<RectTransform>();
            _closeButton.onClick.AddListener(Hide);
            _shadeButton.onClick.AddListener(Hide);
            
            gameObject.SetActive(false);
            _rectTransform.localScale = Vector3.zero;
            _shadeButton.image.color = Color.clear;
            _shadeButton.image.raycastTarget = false;
            
            gameData.OnInputActionEvent?
                .Subscribe(action =>
                {
                    if(action == EActionType.Exit)
                        Hide();
                })
                .AddTo(this);
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            _shadeButton.image.raycastTarget = true;
            _shadeButton.image.DOColor(_windowsConfig.shadeColor, _windowsConfig.showTime);
            _rectTransform.DOScale(Vector3.one, _windowsConfig.showTime);
        }

        public virtual void Hide()
        {
            _shadeButton.image.raycastTarget = false;
            _shadeButton.image.DOColor(Color.clear, _windowsConfig.hideTime);
            _rectTransform.DOScale(Vector3.zero, _windowsConfig.showTime).OnComplete(() => gameObject.SetActive(false));
        }

        private void OnDestroy()
        {
            _closeButton.onClick?.RemoveAllListeners();
            _shadeButton.onClick?.RemoveAllListeners();
        }
    }
}