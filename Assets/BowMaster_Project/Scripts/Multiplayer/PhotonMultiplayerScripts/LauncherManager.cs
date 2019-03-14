using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Zenject;
using System.Collections.Generic;

namespace MultiplayerSystem
{
    public class LauncherManager : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
        [SerializeField]
        private byte maxPlayersInRoom = 2;

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
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To Server");
            multiplayerService.SetConnected();
            //PhotonNetwork.JoinLobby();
            //Debug.Log("this is the master" + PhotonNetwork.IsMasterClient);
        }
        public void PrintPlayerCout()
        {
            Debug.Log("TotalConnected Players " + PhotonNetwork.CountOfPlayers);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected because of: " + cause);

        }



        #endregion
        #region Public Methods
        public void Connect()
        {
            //PhotonNetwork.CreateRoom("testing2", new RoomOptions { MaxPlayers = 2 });
            //Room rooms = PhotonNetwork.;
            Debug.Log("Connecting to room total rooms present " + PhotonNetwork.CountOfRooms);
            if (PhotonNetwork.CountOfRooms == 0)
            {
                Debug.Log("Room created with name testing");
                PhotonNetwork.CreateRoom("testing", new RoomOptions { MaxPlayers = 2 });
            }
            else if (PhotonNetwork.IsConnected)
            {
                Debug.Log("Room joined with Room name testing");
                PhotonNetwork.JoinRoom("testing");
            }
        }
        #endregion
    }
}

