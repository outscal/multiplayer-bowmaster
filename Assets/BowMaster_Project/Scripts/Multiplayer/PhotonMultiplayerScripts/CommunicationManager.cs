using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using InputSystem;
using PlayerSystem;
using GameSystem;

namespace MultiplayerSystem
{
    public class CommunicationManager : IOnEventCallback
    {
        const byte PLAYERSPAWNEVENT=2;
        const byte GAMESTARTEVENT = 3;
        const byte PLAYERHITEVENT = 4;
        const byte INPUTEVENT = 1;
        const byte GAMEOVEREVENT = 5;
        GameRoomManager roomManager;

        PlayerSpawnData spData;
        IMultiplayerService multiplayerService;
        string LastMovePlayerId = " ";
        public CommunicationManager(IMultiplayerService multiplayerService)
        {
            roomManager = GameObject.FindObjectOfType<GameRoomManager>();
            PhotonNetwork.AddCallbackTarget(this);
            this.multiplayerService = multiplayerService;

        }
        ~CommunicationManager()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        void GameOverEventProscess(EventData photonEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            GameOverInfo gameOverInfo = new GameOverInfo();
            gameOverInfo.lostPlayerID = (string)data[0];
            gameOverInfo.reasonToLose = (string)data[1];
            multiplayerService.ChangeToGameOverState(gameOverInfo);
        }
        void GameStartEventProscess()
        {
            Debug.Log("game Started");
            multiplayerService.ChangeToGamePlayState();
            NotifyAllAboutPlayerSpawn(spData);
            LastMovePlayerId=PhotonNetwork.PlayerList[0].UserId;
        }
        void InputEventProscess(EventData photonEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            InputData playerInputData = new InputData();
            playerInputData.playerID = (string)data[0];
            playerInputData.characterID = (int)data[1];
            playerInputData.powerValue = (float)data[2];
            playerInputData.angleValue = (float)data[3];
            multiplayerService.SendInputDataToPlayer(playerInputData,LastMovePlayerId);
            LastMovePlayerId = playerInputData.playerID;
        }
        void PlayerSpawnEventProscess(EventData photonEvent)
        {
            PlayerSpawnData spawnData = new PlayerSpawnData();
            object[] data = (object[])photonEvent.CustomData;
            spawnData.playerPosition = (Vector2)data[1];
            spawnData.playerID = (string)data[0];
            spawnData.char1Health = (float)data[2];
            spawnData.char2Health = (float)data[3];
            spawnData.char3Health = (float)data[4];
            Debug.Log("Spawn Recieved from: " + (string)data[0]);
            roomManager.GameStarted(spawnData.playerID);
            multiplayerService.SpawnPlayer(spawnData);
        }
        void HitEventProscess(EventData photonEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            HitInfo hitInfo = new HitInfo();
            hitInfo.playerId = (string)data[0];
            hitInfo.characterId = (int)data[1];
            hitInfo.characterHealth = (float)data[2];
            multiplayerService.NotifyRemotePlayerHit(hitInfo);
        }

        public void SavePlayerSpawnData(PlayerSpawnData spawnData)
        {
            spData = spawnData;
            spData.playerName = spawnData.playerName;
            spData.char1Health = spawnData.char1Health;
            spData.char2Health = spawnData.char2Health;
            spData.char3Health = spawnData.char3Health;
            //Debug.Log(spawnData.char1Health);
            multiplayerService.SetLocalPlayerID(PhotonNetwork.LocalPlayer.UserId);
            multiplayerService.SetCommunicationManager(this);
        }

        public void NotrifyGameOver(GameOverInfo gameOverInfo)
        {
            //GAMEOVEREVENT
            object[] content = new object[] { gameOverInfo.lostPlayerID, gameOverInfo.reasonToLose };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(GAMEOVEREVENT, content, raiseEventOptions, sendOptions);
        }
        public void NotifyAllAboutPlayerSpawn(PlayerSpawnData spawnData)
        {
            //PLAYERSPAWNEVENT
            //Debug.Log(spawnData.char1Health);
            object[] content = new object[] { spawnData.playerID, spawnData.playerPosition,spawnData.char1Health, spawnData.char2Health, spawnData.char3Health };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(PLAYERSPAWNEVENT, content, raiseEventOptions, sendOptions);
            Debug.Log("Sending SpawnPositions");
        }
        public void NotifyPlayerHit(HitInfo hitData)
        {
            //PLAYERHITEVENT;
            object[] content = new object[] { hitData.playerId, hitData.characterId,hitData.characterHealth };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(PLAYERHITEVENT, content, raiseEventOptions, sendOptions);
        }
        public void NotifyGameStarted()
        {
            //GAMESTARTEVENT
            object[] content = new object[] { };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(GAMESTARTEVENT, content, raiseEventOptions, sendOptions);
        }
        public void SendInputData(InputData data)
        {
            //INPUTEVENT
            if (!LastMovePlayerId.Equals(data.playerID))
            {
                object[] content = new object[] { data.playerID, data.characterID, data.powerValue, data.angleValue };
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                SendOptions sendOptions = new SendOptions { Reliability = true };
                PhotonNetwork.RaiseEvent(INPUTEVENT, content, raiseEventOptions, sendOptions);
            }
        }
        public void OnEvent(EventData photonEvent)
        {
            //Debug.Log("Event Recieved: " + photonEvent.Code);
            byte eventCode = photonEvent.Code;
            switch (eventCode)
            {
                case INPUTEVENT:InputEventProscess(photonEvent); break;
                case PLAYERSPAWNEVENT:PlayerSpawnEventProscess(photonEvent); break;
                case PLAYERHITEVENT: HitEventProscess(photonEvent); break;
                case GAMEOVEREVENT: GameOverEventProscess(photonEvent); break;
                case GAMESTARTEVENT: GameStartEventProscess(); break;
            }
        }
    }
}
