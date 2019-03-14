using MultiplayerSystem;
using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
        private GameObject playerCard;
        private GameObject opponentCard;

        public UIService(IPlayerService playerService, IMultiplayerService multiplayerService, UIScriptableObj uIScriptableObj)
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
        public void ShowGameOverUI(string reason) => uiView.ShowGameOverUI(reason, uIScriptableObj.popUpPrefab);
        public void ShowLobbyUI() => uiView.ShowLobbyUI();
        public void ShowPlayerUI()
        {
            uiView.ShowPlayerUI(uIScriptableObj.playerCard);
            playerCard = GameObject.Instantiate(uIScriptableObj.playerCard);

            //playerCard.GetComponent<>
        }

        // public void ShowDisconnectedUI() => uiView.ShowDisconnectedUI();       

    }
}