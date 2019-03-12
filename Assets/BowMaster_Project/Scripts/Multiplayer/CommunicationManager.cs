using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

namespace MultiplayerSystem
{
    public struct InputData
    {
        public Vector2 spos;
        public string localPlayerID;
        public int characterID;
    }
    public class CommunicationManager :MonoBehaviour, IOnEventCallback
    {
        [SerializeField] lplayer player;
        InputData data = new InputData();
        // Use this for initialization
        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void setData(InputData dat)
        {
            data = dat;
            data.localPlayerID = PhotonNetwork.LocalPlayer.UserId;
            Debug.Log("userId:" + PhotonNetwork.LocalPlayer.UserId);
            byte evCode = 1;
            object[] content = new object[] { data.spos, data.localPlayerID };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
            Debug.Log("Trying to send Event");
        }

        public void spawn(Vector2 data)
        {
            Instantiate(player.gameObject, data, Quaternion.identity);
        }

        public void OnEvent(EventData photonEvent)
        {

            Debug.Log("Event Recieved: " + photonEvent.Code);
            byte eventCode = photonEvent.Code;

            if (eventCode == 1)
            {
                object[] data = (object[])photonEvent.CustomData;
                Vector2 targetPosition = (Vector2)data[0];
                Debug.Log("Spawn Recieved from: " + (string)data[1]);
                spawn(targetPosition);
            }

        }
    }
}
