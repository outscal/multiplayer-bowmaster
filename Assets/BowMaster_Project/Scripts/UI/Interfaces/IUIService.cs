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
        void ShowConnectedUI();
        void ShowDisconnectedUI();
        void ShowVictoryUI();
        void ShowGameOverUI();
    }
}