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

        private GameUIController gameUIController;
        private GameObject mainCanvas;
        private GameObject playerCard;
        private GameObject opponentCard;

        public UIService(IPlayerService playerService, UIScriptableObj uIScriptableObj)
        {
            this.uIScriptableObj = uIScriptableObj;
            //this.uiView = uIScriptableObj.mainUICanvas;
            this.playerService = playerService;
        }

        public void SetMultiplayerServiceRef(IMultiplayerService multiplayerService)
        {            
            this.multiplayerService = multiplayerService;
            SetCanvasReferences();
        }
        public void SetLocalPlayerID(string id)
        {
            localPlayerID = id;
            gameUIController.SetLocalPlayerID(localPlayerID);
        }

        private void SetCanvasReferences()
        {
            mainCanvas = GameObject.FindObjectOfType<UIView>().gameObject;
            uiView = mainCanvas.GetComponent<UIView>();
            mainCanvas.GetComponentInChildren<LobbyController>().SetMultiplayerServiceRef(multiplayerService);
            mainCanvas.GetComponentInChildren<GameUIController>().SetMultiplayerServiceRef(multiplayerService);
            gameUIController = mainCanvas.GetComponentInChildren<GameUIController>();
        }

        public void ShowConnectingUI() => uiView.ShowConnectedUI(uIScriptableObj.popUpPrefab);
        public void ShowGameOverUI(string reason) => uiView.ShowGameOverUI(reason, uIScriptableObj.popUpPrefab);
        public void ShowLobbyUI() => uiView.ShowLobbyUI();

        public void ShowPlayerUI()
        {
            uiView.ShowPlayerUI(uIScriptableObj.playerCard);
            List<string> namesToShow= multiplayerService.GetPlayerNames(localPlayerID);

            playerCard = GameObject.Instantiate(uIScriptableObj.playerCard);
            playerCard.GetComponent<PlayerInfoCardController>().SetPlayerName(namesToShow[0]);
            

            opponentCard = GameObject.Instantiate(uIScriptableObj.playerCard);
            opponentCard.GetComponent<PlayerInfoCardController>().SetPlayerName(namesToShow[1]);

        }


        // public void ShowDisconnectedUI() => uiView.ShowDisconnectedUI();       

    }
}