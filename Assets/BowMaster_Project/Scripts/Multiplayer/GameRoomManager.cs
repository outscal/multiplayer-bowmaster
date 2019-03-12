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
        #region Photon Callbacks
        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
       
        #endregion
        #region Private Methods
        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("Photon trying to Load LevelScene", PhotonNetwork.CurrentRoom.PlayerCount);
            //PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        }

        #endregion
        #region Photon Callbacks
        public override void OnJoinedRoom()
        {
            //base.OnJoinedRoom();
            GameObject.FindObjectOfType<testingUI>().SetPlatform();
            Debug.Log("now this client is in a room total = " + PhotonNetwork.LocalPlayer.NickName + " " + PhotonNetwork.CurrentRoom.Name);
        }
        public override void OnLeftRoom()
        {
            GameObject.FindObjectOfType<testingUI>().ExitRoom();
            //SceneManager.LoadScene(0);
        }
        public override void OnJoinedLobby()
        {
            Debug.Log("In Lobby");
            
        }
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("Player Entered Room NickName:" + other.NickName+ " UserId:" + other.UserId+ " IsMasterClient:" + other.IsMasterClient); // not seen if you're the player connecting
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                LoadArena();
            }
        }
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("Player Left Room:"+ other.NickName); // seen when other disconnects
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                //LoadArena();
            }
        }
        
        #endregion
        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            //PhotonNetwork.JoinLobby();
        }
       
        #endregion
    }
}