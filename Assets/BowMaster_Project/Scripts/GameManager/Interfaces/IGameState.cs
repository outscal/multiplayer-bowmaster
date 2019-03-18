using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace GameSystem
{
    public interface IGameState
    {
        void OnStateEnter();
        void OnStateExit();
        GameStateEnum GetState();

    }
}