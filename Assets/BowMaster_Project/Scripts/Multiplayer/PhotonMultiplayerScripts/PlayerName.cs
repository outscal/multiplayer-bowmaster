using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace MultiplayerSystem
{
    public class PlayerName
    {
        #region Private Serializable Fields

        [SerializeField]
        LauncherManager launch;
        #endregion
        #region Private Constants
        const string playerNamePrefKey = "PlayerName";

        #endregion
        #region MonoBehaviour CallBacks

        #endregion
        #region Public Methods
        public PlayerName()
        {
            launch = GameObject.FindObjectOfType<LauncherManager>();
            string defaultName = string.Empty;
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                //_inputField.text = defaultName;
            }
            PhotonNetwork.NickName = defaultName;
        }
        public void SetPlayerName(string name)
        {
            // #Important
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            //Debug.Log("Player Name entered in input " + name);
            PhotonNetwork.NickName = name;
            PlayerPrefs.SetString(playerNamePrefKey, name);
            launch.Connect();
        }

        #endregion
    }
}