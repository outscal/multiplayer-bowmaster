using UnityEngine;
using System.Collections;
using PlayerSystem;
using UISystem;

namespace GameSystem
{
    public class GameStartState : IGameState
    {
        
        private IUIService uIService;
        private IPlayerService playerService;
        public GameStartState(IUIService uIService, IPlayerService playerService)
        {
            this.uIService = uIService;
            this.playerService = playerService;
        }
        public GameStateEnum GetState()
        {
            return GameStateEnum.GAME_START;
        }

        public void OnStateEnter()
        {
            uIService.ShowConnectingUI();
        }

        public void OnStateExit()
        {
            
        }
    }
}