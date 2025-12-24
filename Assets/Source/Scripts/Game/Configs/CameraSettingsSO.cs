using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(menuName = "Configs/Camera Settings", fileName = "Camera Settings")]
    public class CameraSettingsSO : ScriptableObject
    {
        public float moveSpeed = 12;
        public float rotationSpeed = 10;
        public Vector3 cameraMoveBounds;
        public float minCameraAngleX = -50;
        public float maxCameraAngleX = 20;
        public float maxFOV = 60;
        public float minFOV = 20;
    }
}
