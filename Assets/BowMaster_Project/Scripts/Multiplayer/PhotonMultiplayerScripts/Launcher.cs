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
            Debug.Log("Connected To Server");
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
                //if (PhotonNetwork.IsConnected)
                //{
                //    Debug.Log("Room created or joined with name testing");
                //    PhotonNetwork.JoinOrCreateRoom("testing", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
                //}
            }
        }
        #endregion
    }
}

