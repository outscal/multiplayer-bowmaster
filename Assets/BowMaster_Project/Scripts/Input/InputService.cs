using GameSystem;
using MultiplayerSystem;
using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UISystem;
using UnityEngine;
using Zenject;

namespace InputSystem
{
    public enum InputStatus
    {
        INVALID,
        VALID
    }

    public class InputService : IInputService, ITickable
    {
        private IUIService uiService;
        private IPlayerService playerService;
        private IInputComponent inputComponent;        
        private IGameService gameService;
        private int selectedCharacterID;
        private Vector2 forwardCharacterDirection;
        private Camera cam;

        public InputService(IUIService uiService, IPlayerService playerService, IGameService gameService)
        {
            this.uiService = uiService;
            this.playerService = playerService;            
            this.gameService = gameService;
            cam = Camera.main;

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            inputComponent = new TouchInput(this, gameService);
#elif UNITY_EDITOR || UNITY_STANDALONE
            inputComponent = new MouseInput(this, gameService);
#endif

        }

        public void Tick()
        {
            inputComponent.OnTick();
        }

        public bool CheckForCharacterPresence(Vector2 position)
        {
            if (cam != null)
            {
                Ray hitRay = cam.ScreenPointToRay(position);
                RaycastHit2D hitInfo = Physics2D.GetRayIntersection(hitRay, 100);
                if (hitInfo.collider != null)
                {
                    //Debug.Log("COLLIDER NAME"+hitInfo.collider.name);   
                    if (hitInfo.collider.GetComponent<IPlayerView>() != null)
                    {
                        //Debug.Log("[InputService] PlayerDetected");
                        selectedCharacterID = hitInfo.collider.GetComponent<IPlayerView>().GetCharacterID();
                        forwardCharacterDirection = hitInfo.collider.GetComponent<IPlayerView>().GetForwardDirection();

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

        public string GetLocalPlayerID()
        {
            return playerService.GetLocalPlayerID();
        }

        public void SendPlayerData(InputData inputData, bool recieveInput)
        {
            playerService.SetPlayerData(inputData, recieveInput);
            //multiplayerService.SendNewInput(inputData);
        }

        public Vector2 GetCharacterForwardDirection()
        {
            return forwardCharacterDirection;
        }

        public void SendPlayerDataToServer(InputData inputData)
        {
            playerService.SendInputDataToServer(inputData);
        }
    }
}