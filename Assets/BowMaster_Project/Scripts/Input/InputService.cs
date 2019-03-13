using System.Collections;
using System.Collections.Generic;
using PlayerSystem;
using UISystem;
using MultiplayerSystem;
using GameSystem;
using UnityEngine;
using Zenject;

namespace InputSystem
{
   

    public class InputService : IInputService, ITickable
    {
        private IUIService uiService;
        private IPlayerService playerService;
        private IInputComponent inputComponent;
        private IMultiplayerService multiplayerService;
        private IGameService gameService; 
        private int selectedCharacterID;
        private Vector2 forwardCharacterDirection;
        private Camera cam;

        public InputService(IUIService uiService,IPlayerService playerService,IMultiplayerService multiplayerService, IGameService gameService)
        {
            this.uiService = uiService;
            this.playerService = playerService;
            this.multiplayerService = multiplayerService;
            this.gameService = gameService;
            cam = Camera.main;

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            inputComponent = new TouchInput(this, multiplayerService,gameService);
#elif UNITY_EDITOR || UNITY_STANDALONE
            inputComponent = new MouseInput(this,multiplayerService,gameService);
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
                Ray hitRay = Camera.main.ScreenPointToRay(position);
                RaycastHit2D hitInfo = Physics2D.GetRayIntersection(hitRay, 100);
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.GetComponent<IPlayerView>() != null)
                    {
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
            playerService.SetPlayerData(inputData,recieveInput);
            //multiplayerService.SendNewInput(inputData);
        }

        public Vector2 GetCharacterForwardDirection()
        {
            return forwardCharacterDirection;
        }
    }
}