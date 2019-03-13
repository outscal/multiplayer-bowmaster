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
        private UILobbyView lobbyView;

        public UIService(IMultiplayerService multiplayerService)
        {
            this.multiplayerService = multiplayerService;
            //GameObject.FindObjectOfType<UILobbyView>().multiplayerService = multiplayerService;
        }
    }
}