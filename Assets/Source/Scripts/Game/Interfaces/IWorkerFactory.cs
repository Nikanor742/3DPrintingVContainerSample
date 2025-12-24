using Game.Gameplay;
using UnityEngine;

namespace Game.Interfaces
{
    public interface IWorkerFactory
    {
        Worker Create(Vector3 pos, Quaternion rot);
    }
}