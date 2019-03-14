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

        PlayerSpawnData spData;
        IMultiplayerService multiplayerService;
        string LastMovePlayerId=" ";
        public CommunicationManager(IMultiplayerService multiplayerService)
        {
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
        }
        void InputEventProscess(EventData photonEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            InputData playerInputData = new InputData();
            playerInputData.playerID = (string)data[0];
            playerInputData.characterID = (int)data[1];
            playerInputData.powerValue = (float)data[2];
            playerInputData.angleValue = (float)data[3];
            multiplayerService.SendInputDataToPlayer(playerInputData);
            LastMovePlayerId = playerInputData.playerID;
        }
        void PlayerSpawnEventProscess(EventData photonEvent)
        {
            PlayerSpawnData spawnData = new PlayerSpawnData();
            object[] data = (object[])photonEvent.CustomData;
            spawnData.playerPosition = (Vector2)data[1];
            spawnData.playerID = (string)data[0];
            Debug.Log("Spawn Recieved from: " + (string)data[0]);
            multiplayerService.SpawnPlayer(spawnData);
        }
        void HitEventProscess(EventData photonEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            HitInfo hitInfo = new HitInfo();
            hitInfo.playerId = (string)data[0];
            hitInfo.characterId = (int)data[1];
            hitInfo.damage = (float)data[2];
            multiplayerService.NotifyRemotePlayerHit(hitInfo);
        }

        public void SavePlayerSpawnData(PlayerSpawnData spawnData)
        {
            spData = spawnData;
            spData.playerName = spawnData.playerName;
            multiplayerService.SetCommunicationManager(this);
        }

        public void NotrifyGameOver()
        {

        }
        public void NotifyAllAboutPlayerSpawn(PlayerSpawnData spawnData)
        {
            //PLAYERSPAWNEVENT
            object[] content = new object[] { spawnData.playerID, spawnData.playerPosition };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(PLAYERSPAWNEVENT, content, raiseEventOptions, sendOptions);
            Debug.Log("Sending SpawnPositions");
        }
        public void NotifyPlayerHit(HitInfo hitData)
        {
            //PLAYERHITEVENT;
            object[] content = new object[] { hitData.playerId, hitData.characterId,hitData.damage };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(PLAYERHITEVENT, content, raiseEventOptions, sendOptions);
            Debug.Log("Sending SpawnPositions");
        }
        public void GameStarted()
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
            if (eventCode == INPUTEVENT)
            {
                InputEventProscess(photonEvent);
                //Debug.Log("Spawn Recieved from: " + (string)data[1]);
            }
            else if (eventCode == PLAYERSPAWNEVENT)
            {
                PlayerSpawnEventProscess(photonEvent);
            }
            else if (eventCode == GAMESTARTEVENT)
            {
                GameStartEventProscess();
            }
            else if (eventCode == PLAYERHITEVENT)
            {
                HitEventProscess(photonEvent);
            }
        }
    }
}
