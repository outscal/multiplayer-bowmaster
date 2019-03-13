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
        IMultiplayerService multiplayerService;
        public CommunicationManager(IMultiplayerService multiplayerService)
        {
            PhotonNetwork.AddCallbackTarget(this);
            this.multiplayerService = multiplayerService;
        }
        ~CommunicationManager()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
        public void NotifyAllAboutPlayerSpawn(PlayerSpawnData spawnData)
        {
            byte evCode = 2;
            object[] content = new object[] { spawnData.playerID, spawnData.playerPosition };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
            Debug.Log("Trying to send Event");
        }
        public void SendInputData(InputData data)
        {            
            data.localPlayerID = PhotonNetwork.LocalPlayer.UserId;
            Debug.Log("userId:" + PhotonNetwork.LocalPlayer.UserId);
            byte evCode = 1;
            object[] content = new object[] { data.localPlayerID, data.localPlayerID };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
            Debug.Log("Trying to send Event");
        }
        public void OnEvent(EventData photonEvent)
        {
            //Debug.Log("Event Recieved: " + photonEvent.Code);
            byte eventCode = photonEvent.Code;
            if (eventCode == 1)
            {
                object[] data = (object[])photonEvent.CustomData;
                Vector2 targetPosition = (Vector2)data[0];
                //Debug.Log("Spawn Recieved from: " + (string)data[1]);
            }else if (eventCode == 2)
            {
                PlayerSpawnData spawnData = new PlayerSpawnData();
                object[] data = (object[])photonEvent.CustomData;
                spawnData.playerPosition= (Vector2)data[1];
                spawnData.playerID = (string)data[0];
                Debug.Log("Spawn Recieved from: " + (string)data[0]);
                multiplayerService.SpawnPlayer(spawnData);

            }
        }
    }
}
