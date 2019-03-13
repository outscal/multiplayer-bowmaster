using System.Collections;
using System.Collections.Generic;
using Zenject;
using UISystem;
using UnityEngine;

namespace GameSystem
{
    public class GameService:IGameService,IInitializable
    {
        private IGameStateMachine gameStateMachine;
        private IUIService uIService;

        public GameService(IUIService uIService)
        {
            this.uIService = uIService;
            Initialize();
        }

        public void ChangeToGameOverState()
        {
            gameStateMachine.ChangeGameState(GameStateEnum.GAME_OVER);            
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
            gameStateMachine.ChangeGameState(GameStateEnum.LOADING);
            
        }

        public void ChangeToLobbyState()
        {
            gameStateMachine.ChangeGameState(GameStateEnum.LOBBY);
           
        }

        public void Initialize()
        {
            gameStateMachine = new GameStateMachine(uIService);
            ChangeToGameStartState();
        }

        public GameStateEnum GetGameState()
        {
            return gameStateMachine.GetGameState();
        }
    }
}