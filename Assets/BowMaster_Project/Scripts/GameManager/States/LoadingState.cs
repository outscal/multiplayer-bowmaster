using UnityEngine;
using System.Collections;

namespace GameSystem
{
    public class LoadingState : IGameState
    {
        public GameStateEnum GetState()
        {
            return GameStateEnum.LOADING;
        }

        public void OnStateEnter()
        {
           
        }

        public void OnStateExit()
        {
            
        }
    }
}