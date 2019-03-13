using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using InputSystem;

namespace PlayerSystem
{
    public interface IPlayerService
    { 
        void SetLocalPlayerID(string localPlayerID);
        void PlayerConnected(PlayerSpawnData playerSpawnData);
        void SetPlayerData(InputData inputData);
        string GetLocalPlayerID();
    }
}