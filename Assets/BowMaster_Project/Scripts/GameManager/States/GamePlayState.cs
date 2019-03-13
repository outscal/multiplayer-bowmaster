using UnityEngine;
using System.Collections;
using PlayerSystem;
using UISystem;


namespace GameSystem
{
    public class GamePlayState : IGameState
    {
        private IUIService uIService;
        private IPlayerService playerService;

        public GamePlayState(IUIService uIService, IPlayerService playerService)
        {
            this.uIService = uIService;
            this.playerService = playerService;
        }

        public GameStateEnum GetState()
        {
            return GameStateEnum.GAME_PLAY;
        }

        public void OnStateEnter()
        {
           //UIStartPlayerUI
           //saveSystem.count
        }

        public void OnStateExit()
        {
            
        }
    }
}