using UnityEngine;
using System.Collections;

namespace GameSystem
{
    public class GameState : IGameState
    {
        public GameStateEnum GetState()
        {
            return GameStateEnum.GAME_PLAY;
        }

        public void OnStateEnter()
        {
           
        }

        public void OnStateExit()
        {
            
        }
    }
}