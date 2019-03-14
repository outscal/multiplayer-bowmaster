using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MultiplayerSystem;
using TMPro;
using System;

namespace UISystem
{
    public class LobbyController : MonoBehaviour
    {
        [SerializeField]
        private Button connectButton;
        [SerializeField]
        private InputField inputField;
        [SerializeField]
        private TextMeshProUGUI errorText;

        private void Start()
        {
            errorText.gameObject.SetActive(false);
        }
        public void SetMultiplayerServiceRef(IMultiplayerService multiplayerService)
        {
            connectButton.onClick.AddListener(() => ConnectToServer(multiplayerService));
        }

        private void ConnectToServer(IMultiplayerService multiplayerService)
        {
            if(inputField.text=="")
            {
                errorText.gameObject.SetActive(true);
                errorText.text = "NAME CANNOT BE EMPTY";
            }
            multiplayerService.Connect(inputField.text);
            
        }
    }
}