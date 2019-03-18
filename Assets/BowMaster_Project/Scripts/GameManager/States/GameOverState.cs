using UnityEngine;
using System.Collections;
using PlayerSystem;
using UISystem;

namespace GameSystem
{
    public class GameOverState : IGameState
    {
        private IUIService uIService;
        private IPlayerService playerService;
        public GameOverState(IUIService uIService,IPlayerService playerService)
        {
           this.uIService=uIService;
            this.playerService= playerService;
        }
        public GameStateEnum GetState()
        {
            return GameStateEnum.GAME_OVER;
        }

        public void OnStateEnter()
        {
           
        }

        public void OnStateExit()
        {
            
        }
    }
}