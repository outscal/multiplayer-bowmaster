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

       

        private List<GameObject> panelList = new List<GameObject>();
        private void Start()
        {
            panelList.Add(lobbyPanel);
            panelList.Add(connectingPanel);
            panelList.Add(gamePanel);

            lobbyPanel.SetActive(false);
            connectingPanel.SetActive(false);
            gamePanel.SetActive(false);
        }

        public void ShowConnectedUI()
        {
            connectingPanel.SetActive(true);            
            DeactivateOtherPanels(connectingPanel);
        }

        public void ShowDisconnectedUI()
        {
           
        }

        public void ShowGameOverUI()
        {
           
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