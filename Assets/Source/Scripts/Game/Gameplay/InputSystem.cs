using System;
using Game.Configs;
using Game.Data;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Game.Gameplay
{
    public class InputSystem : IDisposable
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly ActionsSO _actionsConfig;
        private readonly GameData _gameData;

        public InputSystem(ActionsSO actionsConfig, GameData gameData)
        {
            _actionsConfig = actionsConfig;
            _gameData = gameData;
            
            Observable.EveryUpdate()
                .Subscribe(_ => CheckInputs())
                .AddTo(_disposables);
        }

        private void CheckMouseInputs()
        {
            if (Input.GetMouseButtonDown(0))
                _gameData.OnInputActionEvent?.OnNext(EActionType.LeftMouseDown);
            if (Input.GetMouseButtonUp(0))
                _gameData.OnInputActionEvent?.OnNext(EActionType.LeftMouseUp);
            if (Input.GetMouseButtonDown(1))
                _gameData.OnInputActionEvent?.OnNext(EActionType.RightMouseDown);
            if (Input.GetMouseButtonUp(1))
                _gameData.OnInputActionEvent?.OnNext(EActionType.RightMouseUp);
        }

        private void CheckInputs()
        {
            CheckMouseInputs();

            foreach (var gameAction in _actionsConfig.actions)
            {
                if (Input.GetKeyDown(gameAction.keyCode))
                    _gameData.OnInputActionEvent?.OnNext(gameAction.action);
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
