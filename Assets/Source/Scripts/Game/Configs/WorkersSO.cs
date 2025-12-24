using Game.Gameplay;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(menuName = "Configs/Workers", fileName = "Workers")]
    public sealed class WorkersSO : ScriptableObject
    {
        public int refreshWorkersCost = 25;
        [Range(1,10)] public int workersCount = 3;
        [Range(3,10)] public float workersSpeed = 7;
        [Range(100,200)] public float workersAngularSpeed = 120;
        [Range(5,20)] public float workersAcceleration = 8;

        public Worker workerPrefab;

        public string[] manNames;
        public string[] womenNames;
    }
}
