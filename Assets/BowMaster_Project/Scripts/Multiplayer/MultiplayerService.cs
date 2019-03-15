using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using InputSystem;
using PlayerSystem;
using GameSystem;
using UISystem;

namespace MultiplayerSystem
{
    public class MultiplayerService : IMultiplayerService,IInitializable
    {
        IPlayerService playerService;
        IGameService gameService;
        GameRoomManager gameRoomManager;
        PlayerName playerServerName;
        bool connected = false;
        LauncherManager launcher;
        CommunicationManager communicationManager;
        IUIService uiService;
       
        public MultiplayerService(IPlayerService playerService,IGameService gameService,IUIService uiService)
        {
            launcher = GameObject.FindObjectOfType<LauncherManager>();
            this.uiService = uiService;
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
            playerService.SetPlayerHealth(hit);
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
        public void SendInputDataToPlayer(InputData inputData)
        {
            Debug.Log("[MultiplaerService] Sending WeaponInfo to Player");
            playerService.SetPlayerData(inputData, false);
        }
        public void SetCurrentTurn(string nextTurnID)
        {
            playerService.SetTurnId(nextTurnID);
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
            Debug.Log(playerSpawnData.char1Health);
            playerService.PlayerConnected(playerSpawnData);
        }
        public void ChangeToGameOverState(GameOverInfo gameOverInfo)
        {
            gameService.ChangeToGameOverState(gameOverInfo);
        }
        public void ChangeToLobbyState()
        {
            gameService.ChangeToLobbyState();
        }

        public List<string> GetPlayerNames(string localPlayerId)
        {
            return gameRoomManager.GetPlayerNames();
        }
        public void Disconnect()
        {
            launcher.LeaveRoom();
        }
        public void RestartGame()
        {
            gameRoomManager.Restart();
        }

        public void Initialize()
        {
            uiService.SetMultiplayerServiceRef(this);
        }
    }
}