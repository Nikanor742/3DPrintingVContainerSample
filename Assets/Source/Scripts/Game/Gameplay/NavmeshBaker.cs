using Game.Data;
using R3;
using Unity.AI.Navigation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Gameplay
{
    [RequireComponent(typeof(NavMeshSurface))]
    public class NavmeshBaker : MonoBehaviour, IInitializable
    {
        [SerializeField] private NavMeshSurface _navMeshSurface;
        
        [Inject] private readonly GameData _gameData;
        
        public void Initialize()
        {
            _gameData.OnInstallEquipment?
                .Subscribe(_ => BuildNavMesh())
                .AddTo(this);
        }
        
        public void BuildNavMesh()
        {
            _navMeshSurface?.BuildNavMesh();
        }
        
    }
}
