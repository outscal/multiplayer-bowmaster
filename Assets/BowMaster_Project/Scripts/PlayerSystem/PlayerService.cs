using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using InputSystem;
using WeaponSystem;
using MultiplayerSystem;

namespace PlayerSystem
{
    public class PlayerService : IPlayerService
    {
        readonly SignalBus signalBus;
        private Dictionary<string, PlayerController> playerControllerDictionary;
        private ScriptableObjCharacterList playerList;
        private IWeaponService weaponService;
        private IMultiplayerService multiplayerService;

        private string localPlayerID;

        public PlayerService(SignalBus signalBus, ScriptableObjCharacterList playerList,
        IWeaponService weaponService)
        {
            this.signalBus = signalBus;
            this.playerList = playerList;
            playerControllerDictionary = new Dictionary<string, PlayerController>();
            this.weaponService = weaponService;
            //SpawnPlayer("YoYo", new Vector2(-5, 0));
            //localPlayerID = "YoYo";
            //SetLocalPlayerID("YoYo");
        }

        public void SetLocalPlayerID(string localPlayerID, IMultiplayerService multiplayerService)
        {
            this.multiplayerService = multiplayerService;
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

        public ScriptableObjCharacter ReturnPlayerScriptableObj(PlayerCharacterType playerCharacterType)
        {
            for (int i = 0; i < playerList.playerObject.Count; i++)
            {
                if (playerCharacterType == playerList.playerObject[i].playerType)
                    return playerList.playerObject[i];
            }

            return null;
        }

        public string GetLocalPlayerID()
        {
            return localPlayerID;
        }

        public void SendInputDataToServer(InputData inputData)
        {
           
        }
    }
}