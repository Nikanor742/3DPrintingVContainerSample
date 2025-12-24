using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Game.UI
{
    public class ButtonsPanel : MonoBehaviour
    {
        private RectTransform _rectTransform;

        private bool _isShown = true;
        private bool _transitionState;

        public void Init()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        [Button]
        public async UniTask Show()
        {
            if (!_isShown && !_transitionState)
            {
                _transitionState = true;
                gameObject.SetActive(true);
                await _rectTransform.DOAnchorPos(new Vector2(0, 100), 0.2f).ToUniTask();

                _isShown = true;
                _transitionState = false;
            }
        }

        [Button]
        public async UniTask Close()
        {
            if (_isShown && !_transitionState)
            {
                _transitionState = true;
                await _rectTransform.DOAnchorPos(new Vector2(0, -100), 0.2f).ToUniTask();
                gameObject.SetActive(false);

                _isShown = false;
                _transitionState = false;
            }
        }
    }
}
