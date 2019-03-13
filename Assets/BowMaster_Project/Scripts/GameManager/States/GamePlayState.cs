using UnityEngine;
using System.Collections;

namespace GameSystem
{
    public class GamePlayState : IGameState
    {
        public GameStateEnum GetState()
        {
            return GameStateEnum.GAME_PLAY;
        }

        public void OnStateEnter()
        {
           //UIStartPlayerUI
           //saveSystem.count
        }

        public void OnStateExit()
        {
            
        }
    }
}