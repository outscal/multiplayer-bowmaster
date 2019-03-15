using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Zenject;
using PlayerSystem;
using GameSystem;

namespace MultiplayerSystem
{
    public class GameRoomManager : MonoBehaviourPunCallbacks
    {
        [Inject]CommunicationManager communicationManager;
        Dictionary<string, Dictionary<int,float>> inRoomplayers;
        
        string currentTurnId,previousTurnId;
        #region Private Methods

        #endregion
        #region Photon Callbacks
        public void ResetPlayersInRoom()
        {
            inRoomplayers = new Dictionary<string, Dictionary<int, float>>();
        }
        public override void OnJoinedRoom()
        {
            Vector2 pos;
            
            Debug.Log("You Joined a room YourName is " + PhotonNetwork.LocalPlayer.NickName + " RoomNameIs " + PhotonNetwork.CurrentRoom.Name+" PlayersInRoom "+PhotonNetwork.CurrentRoom.PlayerCount);
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                pos = new Vector2(15, 0);
            }
            else
            {
                pos = new Vector2(-15,0);
            }
            PlayerSpawnData spawn = new PlayerSpawnData();
            spawn.playerID = PhotonNetwork.LocalPlayer.UserId;
            spawn.playerPosition = pos;
            spawn.char1Health = 100f;
            spawn.char2Health = 100f;
            spawn.char3Health = 100f;
            spawn.playerName = PhotonNetwork.LocalPlayer.NickName;
            communicationManager.SavePlayerSpawnData(spawn);
            if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                communicationManager.NotifyGameStarted();
            }
           
        }
        public void AddPlayerToRoom(string ID)
        {
            if (inRoomplayers == null)
            {
                currentTurnId = ID;
                inRoomplayers = new Dictionary<string, Dictionary<int, float>>();
            }
            else
            {
                previousTurnId = ID;
            }
            Dictionary<int, float> healths = new Dictionary<int, float>();
            healths.Add(0, 100f);
            healths.Add(1, 100f);
            healths.Add(2, 100f);

            inRoomplayers.Add(ID, healths);
        }
        

        public void playerHit(string hitPlayerID,int charachterID,float damage)
        {
            inRoomplayers[hitPlayerID][charachterID] -= damage;
            HitInfo hitInfo = new HitInfo();
            hitInfo.playerId = hitPlayerID;
            hitInfo.characterHealth = inRoomplayers[hitPlayerID][charachterID];
            hitInfo.characterId = charachterID;
            hitInfo.destroy = false;
            if (inRoomplayers[hitPlayerID][charachterID] < 0)
            {
                hitInfo.destroy = true;
                inRoomplayers[hitPlayerID].Remove(charachterID);
            }
            communicationManager.NotifyPlayerHit(hitInfo);
            if (inRoomplayers[hitPlayerID].Count < 1)
            {
                GameOverInfo overInfo = new GameOverInfo();
                overInfo.lostPlayerID = hitPlayerID;
                overInfo.reasonToLose = "All Characters Dead";
                communicationManager.NotifyGameOver(overInfo);
            }
        }
        public override void OnLeftRoom()
        {
            Debug.Log("You Left Room");
        }
        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("A Player Entered Room With Name: " + other.NickName);
        }
        
        public void Restart()
        {
            PhotonNetwork.LeaveRoom();
        }
        public List<string> GetPlayerNames()
        {
            List<string> names = new List<string>();
            if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.CurrentRoom.Players[1].NickName)
            {
                names.Add(PhotonNetwork.CurrentRoom.Players[1].NickName);
                names.Add(PhotonNetwork.CurrentRoom.Players[2].NickName);
            }
            else
            {
                names.Add(PhotonNetwork.CurrentRoom.Players[2].NickName);
                names.Add(PhotonNetwork.CurrentRoom.Players[1].NickName);
            }
            return names;
        }
        public string GetCurrentTurn()
        {
            return currentTurnId;
        }
        public string ChangeTurn()
        {
            string intermidiateID;
            intermidiateID = currentTurnId;
            currentTurnId = previousTurnId;
            previousTurnId = intermidiateID;
            return currentTurnId;
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