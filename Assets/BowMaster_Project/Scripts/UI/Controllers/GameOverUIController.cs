using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MultiplayerSystem;
using System.Collections;
using System;

namespace UISystem
{
    public class GameOverUIController : MonoBehaviour
    {
        [SerializeField]
        private Button replayButton;
        [SerializeField]
        private Button exitButton;


        private IMultiplayerService multiplayerService;        
        private string localID;

        private void Start()
        {
            replayButton.onClick.AddListener(() => ReplayGame());
            exitButton.onClick.AddListener(() => ExitGame());
        }


        public void SetMultiplayerServiceRef(IMultiplayerService multiplayerService)
        {
            this.multiplayerService = multiplayerService;
        }
        public void SetLocalPlayerID(string id)
        {
            localID = id;
        }
        private void ReplayGame()
        {
            Debug.Log("REPLAY GAME called:");
            multiplayerService.RestartGame(); ;
        }
        private void ExitGame()
        {
            Debug.Log("EXIT GAME called:");
            Application.Quit();
        }
    }
}