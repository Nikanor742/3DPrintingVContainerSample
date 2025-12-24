using System;
using System.Linq;
using Game.Data;
using Game.Gameplay;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(menuName = "Configs/Shop", fileName = "Shop")]
    public class ShopSO : ScriptableObject
    {
        public EquipShopItem[] shopItems;

        public EquipShopItem GetItem(EEquipType item)
        {
            return shopItems.FirstOrDefault(s => s.itemType == item);
        }
    }

    [Serializable]
    public class EquipShopItem
    {
        public EEquipType itemType;
        public Equipment prefab;
        public Sprite shopIcon;
        public int cost;
    }
}

