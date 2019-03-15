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
        string[] rooms = new string[] { "room1", "room2", "room3", "room4" };
        [SerializeField]
        private byte maxPlayersInRoom = 2;
        int room = 0;
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
            PhotonNetwork.LeaveRoom();
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
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("Failed to join room = " + rooms[room]);
            room++;
            Connect();
        }
        public override void OnJoinedLobby()
        {
            room = 0;
            multiplayerService.ChangeToLobbyState();
        }
        #endregion
        #region Public Methods
        public void Connect()
        {
            //PhotonNetwork.CreateRoom("testing2", new RoomOptions { MaxPlayers = 2 });
            //Room rooms = PhotonNetwork.;
            Debug.Log("try Connecting to room " + rooms[room]);
            bool testing = PhotonNetwork.JoinOrCreateRoom(rooms[room], new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
                //while(!PhotonNetwork.JoinOrCreateRoom(rooms[room], new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default))
            //{
            //    room++;
            //    Debug.Log("Failed to enter room- " + rooms[room]);
            //}
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

