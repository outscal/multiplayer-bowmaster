using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace GameSystem
{
    public class GameService:IGameService,IInitializable
    {
        private IGameStateMachine gameStateMachine;
        public GameService()
        {

        }

        public void ChangeToGameOverState()
        {
            
        }

        public void ChangeToGameStartState()
        {
            gameStateMachine.ChangeGameState(GameStateEnum.GAME_START);
        }

        public void ChangeToGamePlayState()
        {
            gameStateMachine.ChangeGameState(GameStateEnum.GAME_PLAY);
            
        }

        public void ChangeToLoadingState()
        {
            
        }

        public void ChangeToLobbyState()
        {
           
        }

        public void Initialize()
        {
            gameStateMachine = new GameStateMachine();
            ChangeToGameStartState();
        }
    }
}