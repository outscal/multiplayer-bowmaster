using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Zenject;
using System.Collections.Generic;
using GameSystem;

namespace MultiplayerSystem
{
    public class LauncherManager : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
        [SerializeField]
        private byte maxPlayersInRoom = 2;
        [Inject] CommunicationManager communicationManager;
        #endregion
        #region Private Fields
        string gameVersion = "1";
        [Inject] IMultiplayerService multiplayerService;
        #endregion
        #region MonoBehaviour CallBacks
        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
        public void LeaveRoom()
        {
            GameOverInfo overInfo = new GameOverInfo();
            overInfo.lostPlayerID = PhotonNetwork.LocalPlayer.UserId;
            overInfo.reasonToLose = "player Disconnected";
            communicationManager.NotifyGameOver(overInfo);
            //PhotonNetwork.Disconnect();
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To Server");
            multiplayerService.SetConnected();
            PhotonNetwork.JoinLobby();
            //Debug.Log("this is the master" + PhotonNetwork.IsMasterClient);
        }
        public void PrintPlayerCout()
        {
            Debug.Log("TotalConnected Players " + PhotonNetwork.CountOfPlayers);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            GameOverInfo overInfo = new GameOverInfo();
            overInfo.lostPlayerID = PhotonNetwork.LocalPlayer.UserId;
            overInfo.reasonToLose = "player Disconnected";
            communicationManager.NotifyGameOver(overInfo);
            Debug.Log("Disconnected because of: " + cause);
        }
        public override void OnLeftRoom()
        {
            multiplayerService.ChangeToLobbyState();
        }
        #endregion
        #region Public Methods
        public void Connect()
        {
            //PhotonNetwork.CreateRoom("testing2", new RoomOptions { MaxPlayers = 2 });
            //Room rooms = PhotonNetwork.;
            
            Debug.Log("Connecting to room total rooms present " + PhotonNetwork.CountOfRooms);
            bool testing=PhotonNetwork.JoinOrCreateRoom("testing", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
            Debug.Log("was able to join room " + testing);
            //if (PhotonNetwork.CountOfRooms == 0)
            //{
            //    Debug.Log("Room created with name testing");
            //    PhotonNetwork.CreateRoom("testing", new RoomOptions { MaxPlayers = 2 });
            //}
            //else if (PhotonNetwork.IsConnected)
            //{
            //    Debug.Log("Room joined with Room name testing");
            //    PhotonNetwork.JoinRoom("testing");
            //}
        }
        #endregion
    }
}

