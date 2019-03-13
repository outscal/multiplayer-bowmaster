using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using InputSystem;
using WeaponSystem;

namespace PlayerSystem
{
    public class PlayerService : IPlayerService
    {
        readonly SignalBus signalBus;
        private Dictionary<string, PlayerController> playerControllerDictionary;
        private ScriptableObjPlayer scriptableObjPlayer;
        private IWeaponService weaponService;

        private string localPlayerID;

        public PlayerService(SignalBus signalBus, ScriptableObjPlayer scriptableObjPlayer,
        IWeaponService weaponService)
        {
            this.signalBus = signalBus;
            this.scriptableObjPlayer = scriptableObjPlayer;
            playerControllerDictionary = new Dictionary<string, PlayerController>();
            this.weaponService = weaponService;
        }

        public void SetLocalPlayerID(string localPlayerID)
        {
            this.localPlayerID = localPlayerID;
            Debug.Log("this is the Localplayer Id that Player Service Recieved " + localPlayerID);
        }

        public void PlayerConnected(PlayerSpawnData playerSpawnData)
        {
            SpawnPlayer(playerSpawnData.playerID, playerSpawnData.playerPosition);
        }

        void SpawnPlayer(string playerID, Vector2 spawnPos)
        {
            PlayerController playerController = new PlayerController(playerID, this
            , weaponService, spawnPos);
            Debug.Log("this is the player Id that Player Service Recieved to add" + playerID);
            playerControllerDictionary.Add(playerID, playerController);
        }

        public void SetPlayerData(InputData inputData, bool gettingInput)
        {
            if (!gettingInput)
            {
                Debug.Log("this is the player Id that Player Service Recieved to move" + inputData.playerID);
            }
            if (playerControllerDictionary.Count > 0 && playerControllerDictionary.ContainsKey(inputData.playerID) )
            {
                //Debug.Log("[PlayerService] Power:" + inputData.powerValue +
                //"\n Angle:" + inputData.angleValue +
                //"\n PlayerID:" + inputData.playerID);
                playerControllerDictionary[inputData.playerID].SetShootInfo(inputData.powerValue
                , inputData.angleValue
                , inputData.characterID
                , gettingInput);
            }
        }

        public ScriptableObjPlayer ReturnPlayerScriptableObj()
        {
            return scriptableObjPlayer;
        }

        public string GetLocalPlayerID()
        {
            return localPlayerID;
        }
    }
}