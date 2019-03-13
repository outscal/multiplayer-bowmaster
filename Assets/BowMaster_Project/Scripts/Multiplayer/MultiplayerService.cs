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
        PlayerName playerServerName;
        Launcher launch;
        CommunicationManager communicationManager;

        public MultiplayerService(IPlayerService playerService)
        {
            playerServerName = new PlayerName();
            launch = GameObject.FindObjectOfType<Launcher>();
            this.playerService = playerService;
            //this.inputService = inputService;
        }

        public void Connect(string name)
        {
            playerServerName.SetPlayerName(name);
        }

        public void SendNewInput(InputData inputData)
        {
            if (communicationManager != null)
                communicationManager.SendInputData(inputData);
        }

        public void SetCommunicationManager(CommunicationManager communicationManager)
        {
            
                this.communicationManager = communicationManager;
        }
        public void SendInputDataToPlayer(InputData inputData)
        {
            playerService.SetPlayerData(inputData, false);
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