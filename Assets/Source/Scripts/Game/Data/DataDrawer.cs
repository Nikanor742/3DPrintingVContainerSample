using NaughtyAttributes;
using UnityEngine;

namespace Game.Data
{
    public class DataDrawer: MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;

        private void Awake()
        {
            _playerData = SaveExtension.player;
        }

        [Button]
        private void AddMoney()
        {
            _playerData.resources[EPlayerResourceType.Coins].Count += 1000;
            SaveExtension.SaveData();
        }
    }
}