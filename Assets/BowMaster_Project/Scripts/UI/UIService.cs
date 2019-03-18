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
        private string localPlayerID,localPlayerName;
        private GameUIController gameUIController;
        private GameOverUIController gameOverUIController;
        private GameObject mainCanvas;
        private GameObject playerCard;
        private GameObject opponentCard;
        private PlayerSpawnSide spawnSide;

        public string GetLocalPlayerName()
        {
            return localPlayerName;
        }
        public UIService(IPlayerService playerService, UIScriptableObj uIScriptableObj)
        {
            this.uIScriptableObj = uIScriptableObj;
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
            gameOverUIController.SetLocalPlayerID(localPlayerID);
        }

        private void SetCanvasReferences()
        {
            mainCanvas = GameObject.FindObjectOfType<UIView>().gameObject;
            uiView = mainCanvas.GetComponent<UIView>();
            mainCanvas.GetComponentInChildren<LobbyController>().SetMultiplayerServiceRef(multiplayerService);
            mainCanvas.GetComponentInChildren<LobbyController>().SetUIServiceRef(this);
            mainCanvas.GetComponentInChildren<GameUIController>().SetMultiplayerServiceRef(multiplayerService);
            mainCanvas.GetComponentInChildren<GameOverUIController>().SetMultiplayerServiceRef(multiplayerService);
            gameUIController = mainCanvas.GetComponentInChildren<GameUIController>();
            gameOverUIController = mainCanvas.GetComponentInChildren<GameOverUIController>();

        }

        public void ShowConnectingUI() => uiView.ShowConnectedUI(uIScriptableObj.popUpPrefab);
        public void ShowGameOverUI(string reason)
        {
            uiView.ShowGameOverUI(reason, uIScriptableObj.popUpPrefab);
            uiView.DestroyObject(playerCard);
            uiView.DestroyObject(opponentCard);
        }
        public void ShowLobbyUI() => uiView.ShowLobbyUI();

        async public void ShowPlayerUI()
        {
            uiView.ShowPlayerUI(uIScriptableObj.playerCard);
            Transform gamePanel = uiView.GetPlayerCardParent();
            List<string> namesToShow = multiplayerService.GetPlayerNames(localPlayerID);


            playerCard = GameObject.Instantiate(uIScriptableObj.playerCard, gamePanel);
            playerCard.GetComponent<PlayerInfoCardController>().SetPlayerName(namesToShow[0]);
            localPlayerName = namesToShow[0];
            opponentCard = GameObject.Instantiate(uIScriptableObj.playerCard, gamePanel);
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



    }
}