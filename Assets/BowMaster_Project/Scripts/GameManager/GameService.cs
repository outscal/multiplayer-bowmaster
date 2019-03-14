using PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;
using Zenject;

namespace GameSystem
{
    public class GameService : IGameService, IInitializable
    {
        private IGameStateMachine gameStateMachine;
        private IUIService uIService;
        private IPlayerService playerService;
        private string localPlayerID;

        //public GameService(IUIService uIService)
        public GameService(IUIService uIService, IPlayerService playerService)
        {
            this.uIService = uIService;
            this.playerService = playerService;
            Initialize();
        }

        public void ChangeToGameOverState(GameOverInfo gameOverInfo)
        {
            gameStateMachine.ChangeGameState(GameStateEnum.GAME_OVER);
            if (localPlayerID != gameOverInfo.lostPlayerID)
            {
                uIService.ShowGameOverUI("******* You Win!!!!!! ******");
            }
            else
            {
                uIService.ShowGameOverUI(gameOverInfo.reasonToLose);
            }
        }

        public void ChangeToGameStartState()
        {
            gameStateMachine.ChangeGameState(GameStateEnum.GAME_START);
            uIService.ShowConnectingUI();
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
            uIService.ShowLobbyUI();

        }

        public void Initialize()
        {
            gameStateMachine = new GameStateMachine(uIService, playerService);
            ChangeToGameStartState();
        }

        public GameStateEnum GetGameState()
        {
            return gameStateMachine.GetGameState();
        }

        public void SetLocalPlayerID(string ID)
        {
            localPlayerID = ID;
            uIService.SetLocalPlayerID(localPlayerID);
        }
    }
}