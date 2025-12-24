using System;
using System.Collections.Generic;
using Game.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay
{
    public sealed class OrdersSystem : IDisposable
    {
        public OrdersSystem()
        {
            GenerateOrders();
        }

        private void GenerateOrders()
        {
            SaveExtension.player.availableOrders = new List<Order>();

            for (int i = 0; i < 3; i++)
            {
                var order = new Order();
                order.modelType = (EModelType)Random.Range(0, Enum.GetNames(typeof(EModelType)).Length);
                order.count = Random.Range(5, 10);
                SaveExtension.player.availableOrders.Add(order);
            }
            SaveExtension.SaveData();
        }
        
        public void Dispose()
        {
            
        }
    }
}