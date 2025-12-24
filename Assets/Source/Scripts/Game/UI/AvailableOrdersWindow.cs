using UnityEngine;

namespace Game.UI
{
    public sealed class AvailableOrdersWindow : Window
    {
        [SerializeField] private AvailableOrderCard _availableOrderCard;
        private int _selectedIndex;
        
        public void SetSelectedIndex(int selectedIndex)
        {
            _selectedIndex =  selectedIndex;
        }
    }
}