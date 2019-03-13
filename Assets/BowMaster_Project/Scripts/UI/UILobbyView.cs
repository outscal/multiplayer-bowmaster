using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MultiplayerSystem;
using Zenject;

namespace UISystem
{
    public class UILobbyView : MonoBehaviour
    {
        [Inject] IMultiplayerService multiplayerService;
        public InputField inputName;
        public GameObject lobbypanal,connectedText;
        

        public void ConnectBtn()
        {
            multiplayerService.Connect(inputName.text);
            lobbypanal.SetActive(false);
        }
        public void Update()
        {
            connectedText.SetActive(multiplayerService.CheckConnection());
        }
    }
}