using System.Collections;
using System.Collections.Generic;
using PlayerSystem;
using UISystem;
using MultiplayerSystem;
using UnityEngine;
using Zenject;

namespace InputSystem
{
    public struct InputData
    {
        public float powerValue;
        public float angleValue;
        public int localPlayerID;
        public int characterID;

    }

    public class InputService : IInputService, ITickable
    {
        private IUIService uiService;
        private IPlayerService playerService;
        private IInputComponent inputComponent;
        private IMultiplayerService multiplayerService;
        private int selectedCharacterID;
        private Camera cam;

        public InputService(IUIService uiService,IPlayerService playerService,IMultiplayerService multiplayerService)
        {
            this.uiService = uiService;
            this.playerService = playerService;
            this.multiplayerService = multiplayerService;
            cam = Camera.main;

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            inputComponent = new TouchInput(this, multiplayerService);
#elif UNITY_EDITOR || UNITY_STANDALONE
            inputComponent = new MouseInput(this,multiplayerService);
#endif

        }

        public void Tick()
        {
            inputComponent.OnTick();
        }

        public bool CheckForCharacterPresence(Vector2 position)
        {
            GameObject hitObject = null;
            if (cam != null)
            {
                Ray ray = cam.ScreenPointToRay(position);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo,500f))
                {
                    hitObject = hitInfo.collider.gameObject;
                    if (hitObject.GetComponent<IPlayerView>()!=null)
                    {
                        selectedCharacterID= hitObject.GetComponent<IPlayerView>().GetCharacterID();
                        return true;
                    }
                }
            }
            return false;
        }
           
        public int GetSelectedCharacterID()
        {
            return selectedCharacterID;
        }

        public int GetLocalPlayerID()
        {
            return playerService.GetLocalPlayerID();
        }

        public void SendPlayerData(InputData inputData)
        {
            Debug.Log("PLAYER DATA Recieved");
            Debug.Log("PLAYER DATA: POWER : :"+inputData.powerValue);
            Debug.Log("PLAYER DATA: ANGLE : :"+inputData.angleValue.ToString());
            Debug.Log("PLAYER DATA: LocalID : :"+inputData.localPlayerID);

            playerService.SetPlayerData(inputData);
        }
    }
}