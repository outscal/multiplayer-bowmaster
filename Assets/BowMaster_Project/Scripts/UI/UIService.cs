using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using MultiplayerSystem;

namespace UISystem
{
    public class UIService : IUIService
    {
        private IMultiplayerService multiplayerService;

        public UIService(IMultiplayerService multiplayerService)
        {
            this.multiplayerService = multiplayerService;
        }
    }
}