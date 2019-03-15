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
using CameraSystem;

namespace Commons
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<SignalDestroyWeapon>();


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

            Container.Bind<ICameraService>().
                To<CameraService>().
                AsSingle().
                NonLazy();

            Container.Bind(typeof(IMultiplayerService), typeof(IInitializable)).
                To<MultiplayerService>().
                AsSingle().
                NonLazy();
            Container.Bind(typeof(IGameService), typeof(IInitializable)).
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