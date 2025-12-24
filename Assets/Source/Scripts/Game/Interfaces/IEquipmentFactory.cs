using Game.Data;
using Game.Gameplay;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IEquipmentFactory
    {
        Equipment Create(EEquipType type, Vector3 pos, Quaternion rot);
    }
}