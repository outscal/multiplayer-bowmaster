using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace UISystem
{
    public interface IUIService
    {
        void ShowPlayerUI();
        void ShowLobbyUI();
        void ShowConnectingUI();
       // void ShowDisconnectedUI();        
        void ShowGameOverUI(string reason);
        void SetLocalPlayerID(string id);
    }
}