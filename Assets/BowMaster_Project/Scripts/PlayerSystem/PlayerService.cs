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
            this.weaponService = weaponService;
            SpawnPlayer("YoYo");
        }

        public void SpawnPlayer(string playerID)
        {
            playerControllerDictionary.Add(playerID, new PlayerController(playerID, this));
        }

        public void SetPlayerData(InputData inputData)
        {
            Debug.Log("[PlayerService] Power:" + inputData.powerValue +
            "/n Angle:" + inputData.angleValue);
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