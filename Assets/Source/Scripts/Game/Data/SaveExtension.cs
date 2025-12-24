using System;
using OdinSerializer;
using UnityEngine;

namespace Game.Data
{
    public static class SaveExtension
    {
        public static PlayerData player;
        private const string _playerDataKey = "PlayerData";
        
        public static void SaveData()
        {
            byte[] bytes = SerializationUtility.SerializeValue(player, DataFormat.Binary);
            PlayerPrefs.SetString(_playerDataKey, Convert.ToBase64String(bytes));
        }
        
        public static void Override()
        {
            try
            {
                if (PlayerPrefs.HasKey(_playerDataKey))
                {
                    byte[] bytes = Convert.FromBase64String(PlayerPrefs.GetString(_playerDataKey));
                    player = SerializationUtility.DeserializeValue<PlayerData>(bytes, DataFormat.Binary);
                    player.InitReactiveProperties();
                }
                else
                {
                    player = new PlayerData();
                }
            }
            catch
            {
                player = new PlayerData();
            }
        }
    }
}