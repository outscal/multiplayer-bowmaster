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
            SpawnPlayer("YoYo");
            localPlayerID = "YoYo";
        }

        public void SpawnPlayer(string playerID)
        {
            PlayerController playerController = new PlayerController(playerID, this, weaponService);
            playerControllerDictionary.Add(playerID, playerController);
        }

        public void SetPlayerData(InputData inputData)
        {
            Debug.Log("[PlayerService] Power:" + inputData.powerValue +
            "\n Angle:" + inputData.angleValue +
            "\n PlayerID:" + inputData.localPlayerID);

            playerControllerDictionary[inputData.localPlayerID].SetShootInfo(inputData.powerValue
            , inputData.angleValue
            , inputData.characterID);
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