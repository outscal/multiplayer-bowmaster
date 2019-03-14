using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using InputSystem;
using PlayerSystem;
using GameSystem;

namespace MultiplayerSystem
{
    public class MultiplayerService : IMultiplayerService
    {
        IPlayerService playerService;
        IGameService gameService;
        GameRoomManager gameRoomManager;
        PlayerName playerServerName;
        bool connected = false;
        CommunicationManager communicationManager;
       
        public MultiplayerService(IPlayerService playerService,IGameService gameService)
        {
            this.gameService = gameService;
            playerServerName = new PlayerName();
            gameRoomManager = GameObject.FindObjectOfType<GameRoomManager>();
            this.playerService = playerService;
            //this.inputService = inputService;
        }
        public void PlayerHit(string hitPlayerID, int characterID, float damage)
        {
            gameRoomManager.playerHit(hitPlayerID, characterID, damage);
        }
        public void NotifyRemotePlayerHit(HitInfo hit)
        {

        }
        public void SetConnected()
        {
            connected = true;
            ChangeToLobbyState();
        }
        public void Connect(string name)
        {
            playerServerName.SetPlayerName(name);
        }
        public bool CheckConnection()
        {
            return connected;
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
        public void SendInputDataToPlayer(InputData inputData,string nextTurnID)
        {
            playerService.SetPlayerData(inputData, false);
            pla
        }
        public void SetLocalPlayerID(string localID)
        {
            gameService.SetLocalPlayerID(localID);
            playerService.SetLocalPlayerID(localID,this);
        }
        public void ChangeToGamePlayState()
        {
            gameService.ChangeToGamePlayState();
        }
        public void SpawnPlayer(PlayerSpawnData playerSpawnData)
        {
            playerService.PlayerConnected(playerSpawnData);
        }
        public void ChangeToGameOverState(GameOverInfo gameOverInfo)
        {
            gameService.ChangeToGameOverState(gameOverInfo);
        }
        public void ChangeToGameDisconnectedState()
        {
            
        }
        public void ChangeToLobbyState()
        {
            gameService.ChangeToLobbyState();
        }
    }
}