using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using PlayerSystem;
using MultiplayerSystem;

namespace UISystem
{
    public class UIService : IUIService
    {
        
        private IUIView uiView;
        private IPlayerService playerService;
        private IMultiplayerService multiplayerService;
        private UIScriptableObj uIScriptableObj;
        private string localPlayerID;
        private GameObject mainCanvas;


        public UIService(IPlayerService playerService , IMultiplayerService multiplayerService,  UIScriptableObj uIScriptableObj)
        {
            this.uIScriptableObj = uIScriptableObj;
            this.uiView = uIScriptableObj.mainUICanvas;
            this.playerService = playerService;
            this.multiplayerService = multiplayerService;
            SpawnUICanvas(uIScriptableObj);
        }

        public void SetLocalPlayerID(string id)
        {
            localPlayerID = id;
            mainCanvas.GetComponentInChildren<GameUIController>().SetLocalPlayerID(localPlayerID);
        }

        private void SpawnUICanvas(UIScriptableObj uIScriptableObj)
        {
            mainCanvas = GameObject.Instantiate(uIScriptableObj.mainUICanvas.gameObject);
            mainCanvas.GetComponentInChildren<LobbyController>().SetMultiplayerServiceRef(multiplayerService);
            mainCanvas.GetComponentInChildren<GameUIController>().SetMultiplayerServiceRef(multiplayerService);
        }

        public void ShowConnectingUI() => uiView.ShowConnectedUI(uIScriptableObj.popUpPrefab);
        public void ShowGameOverUI(string reason) => uiView.ShowGameOverUI(reason,uIScriptableObj.popUpPrefab);        
        public void ShowLobbyUI() => uiView.ShowLobbyUI();        
        public void ShowPlayerUI() => uiView.ShowPlayerUI(uIScriptableObj.playerCard);

       // public void ShowDisconnectedUI() => uiView.ShowDisconnectedUI();       
       
    }
}