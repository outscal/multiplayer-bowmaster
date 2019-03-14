using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using InputSystem;
using PlayerSystem;

namespace MultiplayerSystem
{
    public class CommunicationManager : IOnEventCallback
    {
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
        public void SavePlayerSpawnData(PlayerSpawnData spawnData)
        {
            
            spData = spawnData;
            multiplayerService.SetCommunicationManager(this);
        }
        public void NotifyAllAboutPlayerSpawn(PlayerSpawnData spawnData)
        {
            byte evCode = 2;
            object[] content = new object[] { spawnData.playerID, spawnData.playerPosition };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
            Debug.Log("Sending SpawnPositions");
        }
        public void GameStarted()
        {
            byte evCode = 3;
            object[] content = new object[] { };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
        }
        public void SendInputData(InputData data)
        {
            if (!LastMovePlayerId.Equals(data.playerID))
            {
                byte evCode = 1;
                object[] content = new object[] { data.playerID, data.characterID, data.powerValue, data.angleValue };
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                SendOptions sendOptions = new SendOptions { Reliability = true };
                PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
            }
        }
        public void OnEvent(EventData photonEvent)
        {
            //Debug.Log("Event Recieved: " + photonEvent.Code);
            byte eventCode = photonEvent.Code;
            if (eventCode == 1)
            {
                object[] data = (object[])photonEvent.CustomData;
                InputData playerInputData = new InputData();
                playerInputData.playerID = (string)data[0];
                playerInputData.characterID = (int)data[1];
                playerInputData.powerValue = (float)data[2];
                playerInputData.angleValue = (float)data[3];
                multiplayerService.SendInputDataToPlayer(playerInputData);
                LastMovePlayerId = playerInputData.playerID;
                //Debug.Log("Spawn Recieved from: " + (string)data[1]);
            }
            else if (eventCode == 2)
            {
                PlayerSpawnData spawnData = new PlayerSpawnData();
                object[] data = (object[])photonEvent.CustomData;
                spawnData.playerPosition = (Vector2)data[1];
                spawnData.playerID = (string)data[0];
                Debug.Log("Spawn Recieved from: " + (string)data[0]);
                multiplayerService.SpawnPlayer(spawnData);
            }
            else if (eventCode == 3)
            {
                Debug.Log("game Started");
                NotifyAllAboutPlayerSpawn(spData);
            }
        }
    }
}
