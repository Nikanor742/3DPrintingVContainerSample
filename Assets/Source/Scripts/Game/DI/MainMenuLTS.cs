using Game.Data;
using Game.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.DI
{
    public sealed class MainMenuLTS : LifetimeScope
    {
        [SerializeField] private MainMenuView _mainMenuView;

        protected override void Configure(IContainerBuilder builder)
        {
            SaveExtension.Override();
            
            builder.RegisterComponentInNewPrefab(_mainMenuView, Lifetime.Scoped);
            
            builder.RegisterEntryPoint<MainMenuController>();
        }

    }
}
