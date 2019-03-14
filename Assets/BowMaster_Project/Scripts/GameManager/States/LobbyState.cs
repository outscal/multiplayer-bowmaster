using UnityEngine;
using System.Collections;
using PlayerSystem;
using UISystem;

namespace GameSystem
{
    public class LobbyState : IGameState
    {
        private IUIService uIService;
        private IPlayerService playerService;
        public LobbyState(IUIService uIService, IPlayerService playerService)
        {
            this.uIService = uIService;
            this.playerService = playerService;
        }
        public GameStateEnum GetState()
        {
            return GameStateEnum.LOBBY;
        }

        public void OnStateEnter()
        {
            uIService.ShowLobbyUI();
        }

        public void OnStateExit()
        {
            
        }
    }
}