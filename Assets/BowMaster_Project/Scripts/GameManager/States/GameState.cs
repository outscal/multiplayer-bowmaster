using UnityEngine;
using System.Collections;

namespace GameSystem
{
    public class GameState : IGameState
    {
        public GameStateEnum GetState()
        {
            return GameStateEnum.GAME;
        }

        public void OnStateEnter()
        {
           
        }

        public void OnStateExit()
        {
            
        }
    }
}