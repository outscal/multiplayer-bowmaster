using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace PlayerSystem
{
    public struct InputData
    {
        public float powerValue;
        public float angle;
        public int cahracterID; 
    }

    public class PlayerService : IPlayerService
    {
        readonly SignalBus signalBus;
        private PlayerController playerController;
        private ScriptableObjPlayer scriptableObjPlayer;

        public PlayerService(SignalBus signalBus, ScriptableObjPlayer scriptableObjPlayer)
        {
            this.signalBus = signalBus;
            this.scriptableObjPlayer = scriptableObjPlayer;
        }

        void SpawnPlayer()
        {
            playerController = new PlayerController(2, this); 
        }

        public void SetPlayerData(InputData inputData)
        {

        }

        public ScriptableObjPlayer ReturnPlayerScriptableObj()
        {
            return scriptableObjPlayer;
        }
    }
}