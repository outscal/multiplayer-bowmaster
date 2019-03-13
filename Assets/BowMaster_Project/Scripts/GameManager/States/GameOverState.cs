using UnityEngine;
using System.Collections;

namespace GameSystem
{
    public class GameOverState : IGameState
    {
        public GameStateEnum GetState()
        {
            return GameStateEnum.GAME_OVER;
        }

        public void OnStateEnter()
        {
           
        }

        public void OnStateExit()
        {
            
        }
    }
}