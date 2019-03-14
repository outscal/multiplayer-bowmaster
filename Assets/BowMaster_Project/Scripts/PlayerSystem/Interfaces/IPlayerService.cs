using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using InputSystem;
using MultiplayerSystem;

namespace PlayerSystem
{
    public interface IPlayerService
    { 
        void SetLocalPlayerID(string localPlayerID, IMultiplayerService multiplayerService);
        void PlayerConnected(PlayerSpawnData playerSpawnData);
        void SetPlayerData(InputData inputData, bool gettingInput);
        string GetLocalPlayerID();
        void SendInputDataToServer(InputData inputData);
        
    }
}