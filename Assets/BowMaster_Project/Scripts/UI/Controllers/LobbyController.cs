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

        private IUIService uIService;

        private void Start()
        {
            errorText.gameObject.SetActive(false);
        }
        public void SetMultiplayerServiceRef(IMultiplayerService multiplayerService)
        {
            connectButton.onClick.AddListener(() => ConnectToServer(multiplayerService));
        }

        public void SetUIServiceRef(IUIService uIService)
        {
            this.uIService= uIService;
        }

        private void ConnectToServer(IMultiplayerService multiplayerService)
        {
            if (inputField.text == "")
            {
                errorText.gameObject.SetActive(true);
                errorText.text = "NAME CANNOT BE EMPTY";
            }
            else { multiplayerService.Connect(inputField.text); }
            uIService.ShowWaitingUI();
        }
    }
}