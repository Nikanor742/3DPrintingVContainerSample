using Game.Configs;
using Game.Interfaces;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class WorkerFactory : IWorkerFactory
    {
        private readonly WorkersSO _workersSO;

        public WorkerFactory(WorkersSO workersSO)
        {
            _workersSO = workersSO;
        }
        
        public Worker Create(Vector3 pos, Quaternion rot)
        {
            var newWorker = Object.Instantiate(_workersSO.workerPrefab, pos, rot);
            newWorker.SetNavMeshSettings(_workersSO.workersAngularSpeed, _workersSO.workersAcceleration);
            return newWorker;
        }
    }
}