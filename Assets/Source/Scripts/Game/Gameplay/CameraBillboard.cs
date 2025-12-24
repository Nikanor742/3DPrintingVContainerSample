using UnityEngine;

namespace Game.Gameplay
{
    public class CameraBillboard : MonoBehaviour
    {
        [SerializeField] protected Transform billboardTransform;
        [SerializeField] protected bool billboardX = false;
        [SerializeField] protected bool billboardY = false;
        [SerializeField] protected bool billboardZ = false;
        [SerializeField] protected float offsetToCamera;
        [SerializeField] protected Vector3 localStartPosition;

        private Transform _mainCam;

        protected void Start()
        {
            _mainCam = Camera.main.transform;
            localStartPosition = billboardTransform.localPosition;
        }

        protected void Update()
        {
            billboardTransform.LookAt(billboardTransform.position + _mainCam.rotation * Vector3.forward, _mainCam.rotation * Vector3.up);
            if (!billboardX || !billboardY || !billboardZ)
            {
                billboardTransform.rotation = Quaternion.Euler(billboardX ? billboardTransform.rotation.eulerAngles.x : 0f,
                    billboardY ? billboardTransform.rotation.eulerAngles.y : 0f,
                    billboardZ ? billboardTransform.rotation.eulerAngles.z : 0f);
            }

            billboardTransform.localPosition = localStartPosition;
            billboardTransform.position += billboardTransform.rotation * Vector3.forward * offsetToCamera;
        }
    }
}