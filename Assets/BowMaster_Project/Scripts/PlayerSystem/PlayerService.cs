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

            //SpawnPlayer("YoYo", new Vector2(-5f, 0f));
            //localPlayerID = "YoYo";
        }

        public void SetLocalPlayerID(string localPlayerID)
        {
            this.localPlayerID = localPlayerID;
        }

        public void PlayerConnected(PlayerSpawnData playerSpawnData)
        {
            SpawnPlayer(playerSpawnData.playerID, playerSpawnData.playerPosition);
        }

        void SpawnPlayer(string playerID, Vector2 spawnPos)
        {
            PlayerController playerController = new PlayerController(playerID, this
            , weaponService, spawnPos);
            playerControllerDictionary.Add(playerID, playerController);
        }

        public void SetPlayerData(InputData inputData, bool gettingInput)
        {
            if (playerControllerDictionary.Count>0)
            {
                Debug.Log("[PlayerService] Power:" + inputData.powerValue +
                "\n Angle:" + inputData.angleValue +
                "\n PlayerID:" + inputData.playerID);
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

        public void EndInput(string playerID, int characterID)
        {

        }
    }
}