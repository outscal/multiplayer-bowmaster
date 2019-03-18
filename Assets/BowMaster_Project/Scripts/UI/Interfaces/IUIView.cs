using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace UISystem
{
    public interface IUIView
    {
        void ShowPlayerUI(GameObject playerCard);
        void ShowLobbyUI();
        void ShowConnectedUI(PopUpController popUpController);
       // void ShowDisconnectedUI();        
        void ShowGameOverUI(string reason,PopUpController popUpController);
        void ShowWaitingUI();
        Transform GetPlayerCardParent();
        void DestroyObject(GameObject objectToDestroy);
    }
}