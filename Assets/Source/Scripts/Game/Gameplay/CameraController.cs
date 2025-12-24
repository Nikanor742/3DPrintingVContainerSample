using System;
using Game.Configs;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;
using Observable = R3.Observable;

namespace Game.Gameplay
{
    public class CameraController: IDisposable
    {
        private readonly CameraView _cameraView;
        private readonly CameraSettingsSO _settings;

        private float _rotationX;
        private float _rotationY;
        private float _currentFOV;
        
        private readonly CompositeDisposable _disposable = new ();

        public CameraController(CameraView cameraView, CameraSettingsSO cameraSettings)
        {
            _cameraView = cameraView;
            _settings = cameraSettings;
            
            _currentFOV = _cameraView.virtualCamera.Lens.FieldOfView;

            Observable.EveryUpdate()
                .Subscribe(_ => CheckInputs())
                .AddTo(_disposable);
        }

        private void CheckInputs()
        {
            if (_cameraView.cameraTarget == null) return;

            HandleMovement();
            HandleRotation();
            HandleZoom();
        }

        private void HandleMovement()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            var inputDirection = new Vector3(horizontal, 0, vertical).normalized;
            var moveDirection = _cameraView.cameraTarget.transform.TransformDirection(inputDirection);
            moveDirection.y = 0;

            if (moveDirection.magnitude > 0.1f)
            {
                var newPosition = _cameraView.cameraTarget.position + moveDirection * _settings.moveSpeed * Time.deltaTime;

                newPosition.x = Mathf.Clamp(newPosition.x, -_settings.cameraMoveBounds.x, _settings.cameraMoveBounds.x);
                newPosition.z = Mathf.Clamp(newPosition.z, -_settings.cameraMoveBounds.z, _settings.cameraMoveBounds.z);

                _cameraView.cameraTarget.position = newPosition;
            }
        }

        private void HandleRotation()
        {
            if (Input.GetMouseButton(1))
            {
                var mouseX = Input.GetAxis("Mouse X");
                var mouseY = Input.GetAxis("Mouse Y");

                _rotationY += mouseX * _settings.rotationSpeed;
                _rotationX -= mouseY * _settings.rotationSpeed;
                _rotationX = Mathf.Clamp(_rotationX, _settings.minCameraAngleX, _settings.maxCameraAngleX);

                _cameraView.cameraTarget.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
            }
        }

        private void HandleZoom()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                _currentFOV -= scroll * 40f;
                _currentFOV = Mathf.Clamp(_currentFOV, _settings.minFOV, _settings.maxFOV);
                _cameraView.virtualCamera.Lens.FieldOfView = _currentFOV;
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}