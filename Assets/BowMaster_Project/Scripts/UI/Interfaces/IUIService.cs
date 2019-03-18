using System.Collections;
using System.Collections.Generic;
using Zenject;
using MultiplayerSystem;
using UnityEngine;

namespace UISystem
{
    public interface IUIService
    {
        void SetMultiplayerServiceRef(IMultiplayerService multiplayerService);
        void ShowPlayerUI();
        void ShowLobbyUI();
        void ShowConnectingUI();
        void ShowGameOverUI(string reason);
        void SetLocalPlayerID(string id);
        void ShowWaitingUI();
        // void ShowDisconnectedUI();        
    }
}