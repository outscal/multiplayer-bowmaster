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
        void PlayerHit(HitInfo hit);
        void NotifyRemotePlayerHit(HitInfo hit);
        void ChangeToGameDisconnectedState();
        void ChangeToLobbyState();
        void ChangeToGamePlayState();
        void SendNewInput(InputData inputData);
        void SpawnPlayer(PlayerSpawnData playerSpawnData);
        void SetLocalPlayerID(string localID);
        void Connect(string name);
        bool CheckConnection();
        void SetConnected();
        void SetCommunicationManager(CommunicationManager communicationManager);
        void SendInputDataToPlayer(InputData inputData);
    }
}