using System;
using System.Collections.Generic;
using Game.Gameplay;
using R3;

namespace Game.Data
{
    public class GameData
    {
        //Events
        public readonly Subject<bool> OnShopChangeStateEvent = new();
        public readonly Subject<EActionType> OnInputActionEvent = new();
        public readonly Subject<EEquipType> OnShopEquipmentSelectedEvent = new();
        public readonly Subject<WorkerData> OnWorkerHiredEvent = new();
        public readonly Subject<Unit> OnInstallEquipment = new();
        
        //Objects
        public Dictionary<EEquipType, List<Equipment>> activeObjects = new();

        public GameData()
        {
            foreach (EEquipType item in Enum.GetValues(typeof(EEquipType)))
                activeObjects.Add(item, new List<Equipment>());
        }
    }
}
