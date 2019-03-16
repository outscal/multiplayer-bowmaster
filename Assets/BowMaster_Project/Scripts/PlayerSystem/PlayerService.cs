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
        }

        public PlayerService()
        {
        }

        public void SetLocalPlayerID(string localPlayerID, IMultiplayerService multiplayerService)
        {
            this.multiplayerService = multiplayerService;
            this.localPlayerID = localPlayerID;
            turnID = localPlayerID;
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
            turnID = nextTurnID;
        }

        public void SetPlayerData(InputData inputData, bool gettingInput)
        {
            if (playerControllerDictionary.Count > 0 && playerControllerDictionary.ContainsKey(inputData.playerID))
            {
                playerControllerDictionary[inputData.playerID].SetShootInfo(inputData.powerValue
                , inputData.angleValue
                , inputData.characterID
                , gettingInput);
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

        public void SendPlayerDamageDataToServer(float damage, int characterID,string playerID)
        {
            if (turnID == localPlayerID)
                multiplayerService.PlayerHit(playerID, characterID, damage);
        }

        public void SetPlayerHealth(HitInfo hitInfo)
        {
            playerControllerDictionary[hitInfo.playerId].SetHealth(hitInfo);
        }

        public void ResetPlayerService()
        {
            if(playerControllerDictionary.Count > 0)
            {
                foreach (PlayerController controller in playerControllerDictionary.Values)
                {
                    controller.DestroyPlayer();
                }
                playerControllerDictionary.Clear();
            }
        }

        public bool IsCurrentPlayerTurn()
        {
            //return if its current turn
            return true;
        }

        public List<Vector3> GetCameraPositions()
        {
            List<Vector3> cameraPos = new List<Vector3>();
            cameraPos.Add(new Vector3(-10,0,0));
            cameraPos.Add(new Vector3(10,0,0));
            return cameraPos;
        }
    }
}