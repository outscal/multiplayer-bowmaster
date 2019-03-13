using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace MultiplayerSystem
{
    public class GameRoomManager : MonoBehaviourPunCallbacks
    {
        #region Private Methods
        
        #endregion
        #region Photon Callbacks
        public override void OnJoinedRoom()
        {
            Vector2 pos;
            Debug.Log("now this client is in a room total = " + PhotonNetwork.LocalPlayer.NickName + " " + PhotonNetwork.CurrentRoom.Name);
            if (PhotonNetwork.CurrentRoom.PlayerCount == 0)
            {
                pos = new Vector2(-10, 0);
            }
            else
            {
                pos = new Vector2(10, 0);
            }
        }
        public override void OnLeftRoom()
        {

        }
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("Player Entered Room NickName:" + other.NickName+ " UserId:" + other.UserId+ " IsMasterClient:" + other.IsMasterClient);
        }
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("Player Left Room:"+ other.NickName);
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