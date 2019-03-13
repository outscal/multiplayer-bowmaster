using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using InputSystem;

namespace PlayerSystem
{
    public class PlayerService : IPlayerService
    {
       // readonly SignalBus signalBus;
        private PlayerController playerController;
        private ScriptableObjPlayer scriptableObjPlayer;

        public PlayerService(ScriptableObjPlayer scriptableObjPlayer)//SignalBus signalBus, ScriptableObjPlayer scriptableObjPlayer)
        {
           // this.signalBus = signalBus;
            this.scriptableObjPlayer = scriptableObjPlayer;
            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            playerController = new PlayerController(2.ToString(), this); 
        }

        public void SetPlayerData(InputData inputData)
        {

        }

        public ScriptableObjPlayer ReturnPlayerScriptableObj()
        {
            return scriptableObjPlayer;
        }

        public string GetLocalPlayerID()
        {
            return playerController.ReturnPlayerID();
        }
    }
}