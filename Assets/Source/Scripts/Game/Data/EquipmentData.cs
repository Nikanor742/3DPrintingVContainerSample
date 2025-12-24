using System;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    public class EquipmentData : IEquatable<EquipmentData>
    {
        public EEquipType itemType;
        public Vector3 position;
        public Vector3 rotation;

        public EquipmentData(EEquipType itemType, Vector3 position, Vector3 rotation)
        {
            this.itemType = itemType;
            this.position = position;
            this.rotation = rotation;
        }
        
        public override bool Equals(object obj)
        {
            return Equals(obj as EquipmentData);
        }
    
        public bool Equals(EquipmentData other)
        {
            return other != null 
                   && itemType == other.itemType && position == other.position && rotation == other.rotation;
        }
    
        public override int GetHashCode()
        {
            return HashCode.Combine(itemType, position, rotation);
        }
    }
}