using System.Collections;
using UnityEngine;
using UISystem;
using PlayerSystem;

namespace GameSystem
{
    public class GameStateMachine : IGameStateMachine
    {
        private IGameState currentGameState;
        private IGameState previousGameState;
        private IUIService uIService;
        private IPlayerService playerService;

        public GameStateMachine(IUIService uIService,IPlayerService playerService)
        {
            this.uIService = uIService;
            this.playerService = playerService;
        }

        public void ChangeGameState(GameStateEnum gameState)
        {
            previousGameState = currentGameState;
            switch(gameState)
            {
                case GameStateEnum.GAME_OVER:
                    currentGameState = new GameOverState(uIService, playerService);
                    break;
                case GameStateEnum.GAME_PLAY:
                    currentGameState = new GamePlayState(uIService, playerService);
                    break;
                case GameStateEnum.GAME_START:
                    currentGameState = new GameStartState(uIService, playerService);
                    break;
                case GameStateEnum.LOADING:
                    currentGameState = new LoadingState(uIService, playerService);
                    break;
                case GameStateEnum.LOBBY:
                    currentGameState = new LobbyState(uIService, playerService);
                    break;
            }
           if(previousGameState!=null)
                previousGameState.OnStateExit();
            currentGameState.OnStateEnter();
        }

        public GameStateEnum GetGameState()
        {
            return currentGameState.GetState();
        }
    }
}