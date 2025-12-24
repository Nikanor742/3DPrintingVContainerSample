using System.Linq;
using Game.Configs;
using Game.Data;
using Game.Interfaces;
using UnityEngine;

namespace Game.Gameplay
{
    public class PurchasedEquipmentCreator
    {
        public PurchasedEquipmentCreator(GameData gameData, IEquipmentFactory equipmentFactory)
        {
            foreach (var item in SaveExtension.player.equipmentStorage.purchasedEquipments)
            {
                foreach (var i in item.Value)
                {
                    var newItem = equipmentFactory.Create(i.itemType, i.position, Quaternion.Euler(i.rotation));
                    newItem.SetActive();
                    gameData.activeObjects[i.itemType].Add(newItem);
                }
            }
        }
    }
}