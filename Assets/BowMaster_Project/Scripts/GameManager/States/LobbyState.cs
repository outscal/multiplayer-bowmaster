using UnityEngine;
using System.Collections;

namespace GameSystem
{
    public class LobbyState : IGameState
    {
        public GameStateEnum GetState()
        {
            return GameStateEnum.LOBBY;
        }

        public void OnStateEnter()
        {
           
        }

        public void OnStateExit()
        {
            
        }
    }
}