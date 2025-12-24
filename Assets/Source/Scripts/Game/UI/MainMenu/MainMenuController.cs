using System;
using R3;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Game.UI
{
    public sealed class MainMenuController : IInitializable, IDisposable
    {
        private readonly MainMenuView _mainMenuView;
        private readonly CompositeDisposable _disposables = new();

        public MainMenuController(MainMenuView mainMenuView)
        {
            _mainMenuView = mainMenuView;
        }
        
        public void Initialize()
        {
            _mainMenuView.OnStartClicked
                .Subscribe(_ => StartGame())
                .AddTo(_disposables);

            _mainMenuView.OnSettingsClicked
                .Subscribe(_ => ShowSettings())
                .AddTo(_disposables);

            _mainMenuView.OnExitClicked
                .Subscribe(_ => ExitGame())
                .AddTo(_disposables);
        }

        private void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        private void ShowSettings()
        {
            Debug.Log("Showing settings");
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
