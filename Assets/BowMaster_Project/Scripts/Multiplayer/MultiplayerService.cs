using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using InputSystem;
using PlayerSystem;
using WeaponSystem;
using GameSystem;
using CameraSystem;
using UISystem;

namespace MultiplayerSystem
{
    public class MultiplayerService : IMultiplayerService,IInitializable
    {
        IPlayerService playerService;
        IGameService gameService;
        ICameraService cameraService;
        GameRoomManager gameRoomManager;
        PlayerName playerServerName;
        bool connected = false;
        LauncherManager launcher;
        CommunicationManager communicationManager;
        IUIService uiService;
       
        public MultiplayerService(IPlayerService playerService,IGameService gameService,IUIService uiService, ICameraService cameraService,SignalBus signalBus)
        {
            launcher = GameObject.FindObjectOfType<LauncherManager>();
            this.uiService = uiService;
            this.gameService = gameService;
            this.cameraService = cameraService;
            playerServerName = new PlayerName();
            signalBus.Subscribe<SignalDestroyWeapon>(ChangeTurn);
            gameRoomManager = GameObject.FindObjectOfType<GameRoomManager>();
            this.playerService = playerService;
            
            //this.inputService = inputService;
        }
        void ChangeTurn(SignalDestroyWeapon weapon)
        {
            if (playerService.IsCurrentPlayerTurn())
            {
                Debug.Log("[change turn signal] True");
                communicationManager.NotifyTurnChange();
            }
            else
                Debug.Log("[change turn signal] false");

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
            if (connected == false)
            {
                ChangeToLobbyState();
            }
            connected = true;
            
            
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
            Debug.Log("[MultiplaerService] Sending InputInfo to server");
            playerService.SetPlayerData(inputData, false);
            //cameraService.ResetCameraOrthoSize();
            cameraService.FollowProjectile();

        }
        public void SetCurrentTurn(string nextTurnID)
        {
            playerService.SetTurnId(nextTurnID);
            cameraService.SwitchCamera();
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
            cameraService.OnGameStart();
        }
        public void ChangeToGameOverState(GameOverInfo gameOverInfo)
        {
            gameService.ChangeToGameOverState(gameOverInfo);
            playerService.ResetPlayerService();

        }
        public void ChangeToLobbyState()
        {
            gameService.ChangeToLobbyState();
        }

        public List<string> GetPlayerNames(string localPlayerId)
        {
            return gameRoomManager.GetPlayerNames();
        }
        public void Connect()
        {
            launcher.Connect();
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