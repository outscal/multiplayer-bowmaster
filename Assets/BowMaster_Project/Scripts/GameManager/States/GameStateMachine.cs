using System.Collections;
using UnityEngine;

namespace GameSystem
{
    public class GameStateMachine : IGameStateMachine
    {
        private IGameState currentGameState;

        public GameStateMachine()
        {

        }

        public void ChangeGameState(GameStateEnum gameState)
        {
            switch(gameState)
            {

            }
        }

        public GameStateEnum GetGameState()
        {
            return currentGameState.GetState();
        }
    }
}