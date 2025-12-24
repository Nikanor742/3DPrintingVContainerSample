using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class CurrentOrdersWindow : Window
    {
        public Subject<int> OnFreeCellButtonClick = new();
        
        [SerializeField] private Button[] _freeCellButtons;

        private void Awake()
        {
            for (var i = 0; i < _freeCellButtons.Length; i++)
            {
                var index = i;
                _freeCellButtons[i].onClick.AddListener(() => OnFreeCellButtonClick.OnNext(index));
            }
        }

        public void SetFreeCellVisible(bool isVisible, int index)
        {
            _freeCellButtons[index].transform.parent.gameObject.SetActive(isVisible);
        }
        

        private void OnDestroy()
        {
            foreach (var t in _freeCellButtons)
                t.onClick.RemoveAllListeners();
        }
    }
}