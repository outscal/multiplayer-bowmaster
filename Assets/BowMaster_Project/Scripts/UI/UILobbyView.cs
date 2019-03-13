using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultiplayerSystem;
using Zenject;

namespace UISystem
{
    public class UILobbyView : MonoBehaviour
    {
        [Inject] IMultiplayerService multiplayerService;

        

        public void ConnectBtn()
        {
            multiplayerService.Connect();
        }
    }
}