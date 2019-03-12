using System.Collections;
using System.Collections.Generic;
using Zenject;
using InputSystem;
using MultiplayerSystem;
using PlayerSystem;
using UISystem;
using UnityEngine;

namespace Commons
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerService>().
                To<PlayerService>().
                AsSingle().
                NonLazy();
            Container.Bind<IUIService>().
                To<UIService>().
                AsSingle().
                NonLazy();
            Container.Bind<IMultiplayerService>().
                To<MultiplayerService>().
                AsSingle().
                NonLazy();

            Container.Bind(typeof(IInputService), typeof(ITickable)).
                To<InputService>().
                AsSingle().
                NonLazy();

        }
    }
}