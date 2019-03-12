using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace GameSystem
{
    public interface IGameStateMachine
    {
        GameStateEnum GetGameState();
        void ChangeGameState(GameStateEnum gameState);
    }
}