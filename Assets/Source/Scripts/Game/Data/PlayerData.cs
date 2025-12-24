using System;
using System.Collections.Generic;

namespace Game.Data
{
    [Serializable]
    public class PlayerData
    {
        public Dictionary<EPlayerResourceType, PlayerResource> resources = new();
        public Dictionary<string, WorkerData> hiredWorkers = new();
        
        public EquipmentStorage equipmentStorage = new();
        
        public List<Order> availableOrders = new();
        public List<Order> currentOrders = new();

        public PlayerData()
        {
            resources.Add(EPlayerResourceType.Coins, new PlayerResource());
            resources.Add(EPlayerResourceType.Gems, new PlayerResource());
        }

        public void InitReactiveProperties()
        {
            foreach (var r in resources.Values)
                r.Init();
        }
    }
}
