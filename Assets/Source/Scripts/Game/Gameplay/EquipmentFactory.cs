using Game.Configs;
using Game.Data;
using Game.Gameplay;
using Game.Interfaces;
using UnityEngine;

namespace Source.Scripts.Game.Gameplay
{
    public sealed class EquipmentFactory : IEquipmentFactory
    {
        private readonly ShopSO _shopConfig;

        public EquipmentFactory(ShopSO shopConfig)
        {
            _shopConfig = shopConfig;
        }
        
        public Equipment Create(EEquipType type, Vector3 pos, Quaternion rot)
        {
            var equipConfig = _shopConfig.GetItem(type);
            var newItem = Object.Instantiate(equipConfig.prefab, pos, rot);
            return newItem;
        }
    }
}