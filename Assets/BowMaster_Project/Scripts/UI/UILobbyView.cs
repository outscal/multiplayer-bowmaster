using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultiplayerSystem;
using Zenject;

namespace UISystem
{
    public class UILobbyView : MonoBehaviour
    {
        IMultiplayerService multiplayerService;

        [Inject]
        public UILobbyView(IMultiplayerService mService)
        {
            multiplayerService = mService;
        }

        public void ConnectBtn()
        {
            multiplayerService.Connect();
        }
    }
}