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
        private PlayerSpawnSide spawnSide;

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
            mainCanvas.GetComponentInChildren<LobbyController>().SetUIServiceRef(this);
            mainCanvas.GetComponentInChildren<GameUIController>().SetMultiplayerServiceRef(multiplayerService);
            gameUIController = mainCanvas.GetComponentInChildren<GameUIController>();
        }

        public void ShowConnectingUI() => uiView.ShowConnectedUI(uIScriptableObj.popUpPrefab);
        public void ShowGameOverUI(string reason) => uiView.ShowGameOverUI(reason, uIScriptableObj.popUpPrefab);
        public void ShowLobbyUI() => uiView.ShowLobbyUI();

       async public void ShowPlayerUI()
        {
            uiView.ShowPlayerUI(uIScriptableObj.playerCard);
            List<string> namesToShow = multiplayerService.GetPlayerNames(localPlayerID);
            Debug.Log("Player 1 name" + namesToShow[1]);
            Debug.Log("opponent name" + namesToShow[2]);

            playerCard = GameObject.Instantiate(uIScriptableObj.playerCard);
            playerCard.GetComponent<PlayerInfoCardController>().SetPlayerName(namesToShow[0]);

            opponentCard = GameObject.Instantiate(uIScriptableObj.playerCard);
            opponentCard.GetComponent<PlayerInfoCardController>().SetPlayerName(namesToShow[1]);
            await new WaitForSeconds(0.5f);
            spawnSide = playerService.GetLocalPlayerSide();
            if (spawnSide == PlayerSpawnSide.LEFTSIDE)
            {
                //set left rect

                playerCard.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                playerCard.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                playerCard.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                //set right rect opp
                opponentCard.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
                opponentCard.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                opponentCard.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            }
            else
            {
                //set right player rect
                playerCard.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
                playerCard.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                playerCard.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
                //set left rect opp
                opponentCard.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                opponentCard.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                opponentCard.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            }

        }

        public void ShowWaitingUI() => uiView.ShowWaitingUI();



        // public void ShowDisconnectedUI() => uiView.ShowDisconnectedUI();       

    }
}