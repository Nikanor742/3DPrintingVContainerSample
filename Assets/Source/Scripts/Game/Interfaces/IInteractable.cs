using Game.Data;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IInteractable
    {
        bool Interact();
        
        EInteractType InteractType { get; }
        Transform Transform { get; }
    }
}