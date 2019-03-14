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
        private UIScriptableObj uIScriptableObj;
        
        public UIService(IPlayerService playerService,UIScriptableObj uIScriptableObj)
        {
            this.uIScriptableObj = uIScriptableObj;
            this.uiView = uIScriptableObj.mainUICanvas;
            this.playerService = playerService;
            SpawnUICanvas(uIScriptableObj);
        }

        private void SpawnUICanvas(UIScriptableObj uIScriptableObj)
        {
            GameObject mainCanvas = GameObject.Instantiate(uIScriptableObj.mainUICanvas.gameObject);

        }

        public void ShowConnectingUI() => uiView.ShowConnectedUI(uIScriptableObj.popUpPrefab);       

        public void ShowDisconnectedUI()
        {
           
        }

        public void ShowGameOverUI()
        {
           
        }

        public void ShowLobbyUI() => uiView.ShowLobbyUI();        

        public void ShowPlayerUI() => uiView.ShowPlayerUI(uIScriptableObj.playerCard);

        public void ShowVictoryUI()
        {
            
        }
    }
}