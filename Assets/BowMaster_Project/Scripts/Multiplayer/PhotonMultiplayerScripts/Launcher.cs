using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
namespace MultiplayerSystem
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
        [SerializeField]
        private byte maxPlayersInRoom = 2;

        #endregion
        #region Private Fields
        string gameVersion = "1";

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
            Debug.Log("OnConnected Callback");
           // Debug.Log(PhotonNetwork.CountOfPlayers);
            Debug.Log("this is the master" + PhotonNetwork.IsMasterClient);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("Disconnected because of: " + cause);
        }
       

        #endregion
        #region Public Methods
        public void Connect()
        {
            Debug.Log("Player Count "+PhotonNetwork.CountOfPlayers);
            //Debug.Log("Connecting to room");
            //if (PhotonNetwork.CountOfRooms == 0)
            //{
            //    Debug.Log("Room created with name testing");
            //    PhotonNetwork.CreateRoom("testing", new RoomOptions { MaxPlayers = 4 });
            //}
            //else if (PhotonNetwork.IsConnected)
            //{
            //    PhotonNetwork.JoinRoom("testing");
            //}
        }
        #endregion


    }
}

