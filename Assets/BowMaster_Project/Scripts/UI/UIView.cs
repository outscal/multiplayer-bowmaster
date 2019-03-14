using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MultiplayerSystem;

namespace UISystem
{
    public class UIView : MonoBehaviour,IUIView
    {
        [SerializeField]
        private GameObject lobbyPanel;
        [SerializeField]
        private GameObject connectingPanel;
        [SerializeField]
        private GameObject gamePanel;
        [SerializeField]
        private GameObject gameOverPanel;

        private GameObject popupInstance;

        private List<GameObject> panelList = new List<GameObject>();
        private void Start()
        {
            panelList.Add(lobbyPanel);
            panelList.Add(connectingPanel);
            panelList.Add(gamePanel);
            panelList.Add(gameOverPanel);
           
        }

        public void ShowConnectedUI(PopUpController popUpController)
        {
            DeactivateOtherPanels(connectingPanel);
            connectingPanel.SetActive(true);
            popupInstance = GameObject.Instantiate(popUpController.gameObject);
            popupInstance.SetActive(true);
            popupInstance.transform.SetParent(connectingPanel.transform);
            popUpController.SetText("Connecting To Server.  .  .  .");
        }

        public void ShowDisconnectedUI()
        {
           
        }

        public void ShowGameOverUI()
        {
            gameOverPanel.SetActive(true);
        }

        public void ShowWaitingUI()
        {
            DeactivateOtherPanels(connectingPanel);
            connectingPanel.SetActive(true);
           popupInstance.GetComponent<PopUpController>().SetText("Waiting for players to join . .  .  .  .");

        }

        public void ShowLobbyUI()
        {
            lobbyPanel.SetActive(true);
        }

        public void ShowPlayerUI(GameObject playerCard)
        {
            gamePanel.SetActive(true);
            GameObject playerCardInstance = GameObject.Instantiate(playerCard);
            playerCardInstance.transform.SetParent(gamePanel.transform);
        }

        public void ShowVictoryUI()
        {
            
        }

        private void DeactivateOtherPanels(GameObject currentActivePanel)
        {
            foreach(GameObject panel in panelList)
            {
                if(panel!=currentActivePanel)
                {
                    panel.SetActive(false);
                }
            }
        }
    }
}