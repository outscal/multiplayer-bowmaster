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
        private GameObject gameOverPopUp;

        private List<GameObject> panelList = new List<GameObject>();

        private void Start()
        {
            panelList.Add(lobbyPanel);
            panelList.Add(connectingPanel);
            panelList.Add(gamePanel);
            panelList.Add(gameOverPanel);
            DeactivateOtherPanels(connectingPanel);

        }

        public void ShowConnectedUI(PopUpController popUpController)
        {
          
           // DeactivateOtherPanels(connectingPanel);
           
            popupInstance = GameObject.Instantiate(popUpController.gameObject, connectingPanel.transform);
           
           // popupInstance.transform.SetParent(connectingPanel.transform);
            popupInstance.GetComponent<PopUpController>().SetText("Connecting To Server.  .  .  .");
        }

      //  public void ShowDisconnectedUI()
        //{
            //DeactivateOtherPanels(gameOverPanel);
            //gameOverPanel.SetActive(true);            
            //gameOverPopUp.SetActive(true);
            //popupInstance.transform.SetParent(gameOverPanel.transform);
            //gameOverPopUp.GetComponent<PopUpController>().SetText("Disconnected");
        //}

        public void ShowGameOverUI(string reason, PopUpController popUpController)
        {
            DeactivateOtherPanels(gameOverPanel);
            gameOverPanel.SetActive(true);
            gameOverPopUp = GameObject.Instantiate(popUpController.gameObject);
            gameOverPopUp.SetActive(true);
            popupInstance.transform.SetParent(gameOverPanel.transform);
            gameOverPopUp.GetComponent<PopUpController>().SetText(reason);

        }

        public void ShowWaitingUI()
        {
            DeactivateOtherPanels(connectingPanel);
            connectingPanel.SetActive(true);
            popupInstance.GetComponent<PopUpController>().SetText("Waiting for players to join . .  .  .  .");

        }

        public void ShowLobbyUI()
        {
            DeactivateOtherPanels(lobbyPanel);
            lobbyPanel.SetActive(true);
        }

        public void ShowPlayerUI(GameObject playerCard)
        {
            gamePanel.SetActive(true);
            DeactivateOtherPanels(gamePanel);
            GameObject playerCardInstance = GameObject.Instantiate(playerCard);
            playerCardInstance.transform.SetParent(gamePanel.transform);
        }
      
        private void DeactivateOtherPanels(GameObject currentActivePanel)
        {
            foreach(GameObject panel in panelList)
            {
                if(panel.name!=currentActivePanel.name)
                {
                    panel.SetActive(false);
                }
            }
        }

        public Transform GetPlayerCardParent()
        {
            return gamePanel.transform;
        }
    }
}