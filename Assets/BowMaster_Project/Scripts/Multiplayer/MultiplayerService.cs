using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using InputSystem;
using PlayerSystem;

namespace MultiplayerSystem
{
    public class MultiplayerService : IMultiplayerService
    {
        IPlayerService playerService;
        Launcher launch;

        public MultiplayerService(IPlayerService playerService)
        {
            launch = GameObject.FindObjectOfType<Launcher>();
            this.playerService = playerService;
            //this.inputService = inputService;
        }

        public void Connect()
        {
            launch.Connect();
        }

        public void SendNewInput(InputData inputData)
        {

        }

        public void SetLocalPlayerID(string localID)
        {
            playerService.SetLocalPlayerID(localID);
        }

        public void SpawnPlayer(PlayerSpawnData playerSpawnData)
        {
            playerService.PlayerConnected(playerSpawnData);
        }
    }
}