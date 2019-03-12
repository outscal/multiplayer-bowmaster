using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
namespace MultiplayerSystem
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
        [SerializeField]
        private byte maxPlayersInRoom = 4;

        #endregion
        #region Private Fields


        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "1";

        #endregion
        #region MonoBehaviour CallBacks
        
        public override void OnConnectedToMaster()
        {
            //base.OnConnectedToMaster()
            Debug.Log("OnConnected Callback");
            
            Debug.Log("this is the master"+PhotonNetwork.IsMasterClient);
        }
        
        public override void OnDisconnected(DisconnectCause cause)
        {
            //base.OnDisconnected(cause);
            Debug.Log("Disconnected because of" + cause);
        }
        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>

        #endregion


        #region Public Methods


        /// <summary>
        /// Start the connection process.
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect()
        {
            Debug.Log("Connecting to room");
            if (PhotonNetwork.CountOfRooms == 0)
            {
                Debug.Log("Room created with name testing");
                PhotonNetwork.CreateRoom("testing", new RoomOptions { MaxPlayers = 4 });
            }else if (PhotonNetwork.IsConnected)
            {
                
                
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                PhotonNetwork.JoinRoom("testing");
            }
            //else
            //{
            //    // #Critical, we must first and foremost connect to Photon Online Server.
            //    PhotonNetwork.GameVersion = gameVersion;
            //    PhotonNetwork.ConnectUsingSettings();
            //}
        }


        #endregion


    }
}

