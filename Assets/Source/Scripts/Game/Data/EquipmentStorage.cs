using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    public class EquipmentStorage
    {
        public Dictionary<EEquipType, HashSet<EquipmentData>> purchasedEquipments = new ();

        public EquipmentStorage()
        {
            foreach (EEquipType item in Enum.GetValues(typeof(EEquipType)))
                purchasedEquipments.TryAdd(item, new HashSet<EquipmentData>());
        }

        public void AddEquipment(EquipmentData equipment)
        {
            if (!purchasedEquipments[equipment.itemType].Add(equipment))
                Debug.LogError($"Item {equipment.itemType} is already purchased");
        }

        public void SetPosition(EquipmentData oldEquipment, EquipmentData newEquipment)
        {
            purchasedEquipments[oldEquipment.itemType].Remove(oldEquipment);
            purchasedEquipments[newEquipment.itemType].Add(newEquipment);
        }
        
        public void RemoveItem(EquipmentData equipment)
        {
            if (!purchasedEquipments[equipment.itemType].Remove(equipment))
                Debug.LogError($"Item {equipment.itemType} does not exist in purchased items");
        }
    }
}