using System.Collections;
using System.Collections.Generic;
using Zenject;
using InputSystem;
using UnityEngine;

namespace MultiplayerSystem
{
    public interface IMultiplayerService
    {
        void SendNewInput(InputData inputData);
    }
}