using System.Collections;
using System.Collections.Generic;
using Zenject;
using InputSystem;
using MultiplayerSystem;
using GameSystem;
using PlayerSystem;
using UISystem;
using UnityEngine;
using WeaponSystem;

namespace Commons
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<IPlayerService>().
                To<PlayerService>().
                AsSingle().
                NonLazy();
            Container.Bind<CommunicationManager>().
                To<CommunicationManager>().
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
            Container.Bind<IGameService>().
                To<GameService>().
                AsSingle().
                NonLazy();

            Container.Bind(typeof(IInputService), typeof(ITickable)).
                To<InputService>().
                AsSingle().
                NonLazy();

            Container.Bind(typeof(IWeaponService))
                .To<WeaponService>()
                .AsSingle()
                .NonLazy();

        }
    }
}