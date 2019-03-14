using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace GameSystem
{
    public interface IGameService
    {
        void ChangeToGameStartState();
        void ChangeToLoadingState();
        void ChangeToLobbyState();
        void ChangeToGameOverState(GameOverInfo gameOverInfo);
        void SetLocalPlayerID(string ID);
        void ChangeToGamePlayState();
        GameStateEnum GetGameState();
    }
}