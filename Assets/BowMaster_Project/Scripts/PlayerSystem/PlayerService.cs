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
        private string turnID;

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
            turnID = localPlayerID;
            Debug.Log("this is the Localplayer Id that Player Service Recieved " + localPlayerID);
        }

        public void PlayerConnected(PlayerSpawnData playerSpawnData)
        {
            SpawnPlayer(playerSpawnData);
        }

        public PlayerSpawnSide GetLocalPlayerSide()
        {
            if(playerControllerDictionary[localPlayerID].GetSpawnPos().x < 0)
            {
                return PlayerSpawnSide.LEFTSIDE; 
            }

            return PlayerSpawnSide.RIGHTSIDE;
        }

        void SpawnPlayer(PlayerSpawnData playerSpawnData)
        {
            PlayerController playerController = new PlayerController(playerSpawnData, this
            , weaponService);

            Debug.Log("this is the player Id that Player Service Recieved to add" 
            + playerSpawnData.playerID);

            playerControllerDictionary.Add(playerSpawnData.playerID, playerController);
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

        public void SetTurnId(string nextTurnID)
        {
            Debug.Log("next turn for "+nextTurnID);
            turnID = nextTurnID;
        }

        public void SetPlayerData(InputData inputData, bool gettingInput)
        {
            if (turnID == localPlayerID)
            {
                if (!gettingInput)
                {
                    Debug.Log("this is the player Id that Player Service Recieved to move" + inputData.playerID);
                }
                if (playerControllerDictionary.Count > 0 && playerControllerDictionary.ContainsKey(inputData.playerID))
                {
                    playerControllerDictionary[inputData.playerID].SetShootInfo(inputData.powerValue
                    , inputData.angleValue
                    , inputData.characterID
                    , gettingInput);
                }
            }
        }

        public void SendInputDataToServer(InputData inputData)
        {
            if(turnID == localPlayerID)
            {
                multiplayerService.SendNewInput(inputData);
                playerControllerDictionary[inputData.playerID].DeactivateDisplayPanel();
            }
        }

        public void SendPlayerDamageDataToServer(float damage, int characterID)
        {
            multiplayerService.PlayerHit(localPlayerID, characterID, damage);
        }

        public void SetPlayerHealth(HitInfo hitInfo)
        {
            playerControllerDictionary[hitInfo.playerId].SetHealth(hitInfo.characterId, hitInfo.characterHealth);
        }
    }
}