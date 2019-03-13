using System.Collections;
using UnityEngine;
using UISystem;

namespace GameSystem
{
    public class GameStateMachine : IGameStateMachine
    {
        private IGameState currentGameState;
        private IUIService uIService;

        public GameStateMachine(IUIService uIService)
        {
            this.uIService = uIService;
        }

        public void ChangeGameState(GameStateEnum gameState)
        {
            switch(gameState)
            {
                case GameStateEnum.GAME_OVER:
                    currentGameState = new GameOverState();
                    break;
                case GameStateEnum.GAME_PLAY:
                    currentGameState = new GamePlayState();
                    break;
                case GameStateEnum.GAME_START:
                    currentGameState = new GameStartState();
                    break;
                case GameStateEnum.LOADING:
                    currentGameState = new LoadingState();
                    break;
                case GameStateEnum.LOBBY:
                    currentGameState = new LobbyState();
                    break;
            }
        }

        public GameStateEnum GetGameState()
        {
            return currentGameState.GetState();
        }
    }
}