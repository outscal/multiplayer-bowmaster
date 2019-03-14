using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MultiplayerSystem;
using System.Collections;
using System;

namespace UISystem
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField]
        private Button pauseButton;
        [SerializeField]
        private Button continueButton;
        [SerializeField]
        private Button forfeitButton;
        [SerializeField]
        private GameObject pauseMenu;

        private IMultiplayerService multiplayerService;
        private string localID;
        
        private void Start()
        {
            pauseButton.onClick.AddListener(() => ShowPauseMenu());
            continueButton.onClick.AddListener(() => HidePauseMenu());
            forfeitButton.onClick.AddListener(() => ExitGame());
        }

        public void SetMultiplayerServiceRef(IMultiplayerService multiplayerService)
        {
            this.multiplayerService = multiplayerService;
        }
        public void SetLocalPlayerID(string id)
        {
            localID = id;
        }
        private void ExitGame()
        {
            HidePauseMenu();
            //multiplayerservice.disconnectlocalplayerid;
        }

        private void HidePauseMenu()
        {
            pauseMenu.SetActive(false);
        }

        private void ShowPauseMenu()
        {
            pauseMenu.SetActive(true);       
        }
    }
}