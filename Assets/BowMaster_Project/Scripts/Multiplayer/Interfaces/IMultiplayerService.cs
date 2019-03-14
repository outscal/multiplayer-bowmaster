using System.Collections;
using System.Collections.Generic;
using Zenject;
using InputSystem;
using UnityEngine;
using PlayerSystem;
using GameSystem;

namespace MultiplayerSystem
{
    public interface IMultiplayerService
    {

        void ChangeToGameOverState(GameOverInfo gameOverInfo);
        void PlayerHit(string hitPlayerID, int characterID, float damage);
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