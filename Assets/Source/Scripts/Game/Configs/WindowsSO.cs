using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(menuName = "Configs/Windows Settings", fileName = "WindowsSettings")]
    public sealed class WindowsSO : ScriptableObject
    {
        public Color shadeColor;
        public float showTime;
        public float hideTime;
    }
}