using System.Collections;
using System.Collections.Generic;
using Zenject;
using InputSystem;
using UnityEngine;
using PlayerSystem;

namespace MultiplayerSystem
{
    public interface IMultiplayerService
    {
        void SendNewInput(InputData inputData);
        void SpawnPlayer(PlayerSpawnData playerSpawnData);
        void SetLocalPlayerID(string localID);
        void Connect(string name);
        void SetCommunicationManager(CommunicationManager communicationManager);
        void SendInputDataToPlayer(InputData inputData);
    }
}