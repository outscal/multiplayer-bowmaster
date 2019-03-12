using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace InputSystem
{
    public interface IInputService
    {
        bool CheckForCharacterPresence(Vector2 position);
        int GetSelectedCharacterID();
        int GetLocalPlayerID();
        void SendPlayerData(InputData inputData);
    }
}