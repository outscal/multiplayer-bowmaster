using UnityEngine;
using System.Collections;

namespace GameSystem
{
    public class GameStartState : IGameState
    {
        public GameStateEnum GetState()
        {
            return GameStateEnum.GAME_START;
        }

        public void OnStateEnter()
        {
           
        }

        public void OnStateExit()
        {
            
        }
    }
}