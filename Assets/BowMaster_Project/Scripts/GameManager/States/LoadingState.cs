using UnityEngine;
using System.Collections;
using PlayerSystem;
using UISystem;
namespace GameSystem
{
    public class LoadingState : IGameState
    {
        private IUIService uIService;
        private IPlayerService playerService;
        public LoadingState(IUIService uIService, IPlayerService playerService)
        {
            this.uIService = uIService;
            this.playerService = playerService;
        }
        public GameStateEnum GetState()
        {
            return GameStateEnum.LOADING;
        }

        public void OnStateEnter()
        {
           
        }

        public void OnStateExit()
        {
            
        }
    }
}