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
        void RestartGame();
        List<string> GetPlayerNames(string localPlayerId);
        void ChangeToGameOverState(GameOverInfo gameOverInfo);
        void PlayerHit(string hitPlayerID, int characterID, float damage);
        void NotifyRemotePlayerHit(HitInfo hit);
        void ChangeToLobbyState();
        void ChangeToGamePlayState();
        void SendNewInput(InputData inputData);
        void SpawnPlayer(PlayerSpawnData playerSpawnData);
        void SetLocalPlayerID(string localID);
        void Connect(string name);
        void Connect();
        bool CheckConnection();
        void SetConnected();
        void SetCommunicationManager(CommunicationManager communicationManager);
        void SendInputDataToPlayer(InputData inputData);
        void SetCurrentTurn(string nextTurnID);
    }
}