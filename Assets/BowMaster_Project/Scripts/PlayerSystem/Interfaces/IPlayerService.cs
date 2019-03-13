using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using InputSystem;

namespace PlayerSystem
{
    public interface IPlayerService
    {
        void SpawnPlayer(string playerID);
        void SetPlayerData(InputData inputData);
        string GetLocalPlayerID();
    }
}