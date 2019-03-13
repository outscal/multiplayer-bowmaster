using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Zenject;
using PlayerSystem;

namespace MultiplayerSystem
{
    public class GameRoomManager : MonoBehaviourPunCallbacks
    {
        [Inject] IMultiplayerService multiplayerService;
        [Inject]CommunicationManager communicationManager;
        #region Private Methods

        #endregion
        #region Photon Callbacks
        public override void OnJoinedRoom()
        {
            Vector2 pos;
            Debug.Log("You Joined a room YourName is " + PhotonNetwork.LocalPlayer.NickName + " RoomNameIs " + PhotonNetwork.CurrentRoom.Name+" PlayersInRoom "+PhotonNetwork.CurrentRoom.PlayerCount);
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                pos = new Vector2(5, 0);
            }
            else
            {
                pos = new Vector2(-5,0);
            }
           
            multiplayerService.SetLocalPlayerID(PhotonNetwork.LocalPlayer.UserId);
            PlayerSpawnData spawn = new PlayerSpawnData();
            spawn.playerID = PhotonNetwork.LocalPlayer.UserId;
            spawn.playerPosition = pos;
            communicationManager.SavePlayerSpawnData(spawn);
            if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                communicationManager.GameStarted();
            }
        }
        public override void OnLeftRoom()
        {
            Debug.Log("You Left Room");
        }
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("A Player Entered Room With Name: " + other.NickName);
        }
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("A Player Left Room: "+ other.NickName);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            }
        }
        
        #endregion
        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
       
        #endregion
    }
}