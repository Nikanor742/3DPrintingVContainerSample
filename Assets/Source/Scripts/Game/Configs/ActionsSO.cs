using System;
using Game.Data;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(menuName = "Configs/Actions", fileName = "Actions")]
    public class ActionsSO : ScriptableObject
    {
        public GameAction[] actions;
    }

    [Serializable]
    public class GameAction
    {
        public EActionType action;
        public KeyCode keyCode;
    }
}
