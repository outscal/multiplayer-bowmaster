using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace PlayerSystem
{
    public class PlayerController : IPlayerController
    {
        private string playerID;

        private List<PlayerCharacterController> playerCharacterControllerList;
        private PlayerService playerService;
        private IWeaponService weaponService;
        private Vector2 spawnCharacterPos;
        private string turnID;
        private Vector2 fixedPos;

        public PlayerController(string playerID, PlayerService playerService
        , IWeaponService weaponSystem, Vector2 spawnPos)
        {
            this.playerService = playerService;
            this.weaponService = weaponSystem;
            this.playerID = playerID;
            spawnCharacterPos = spawnPos;
            fixedPos = spawnPos;
            playerCharacterControllerList = new List<PlayerCharacterController>();

            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    PlayerCharacterController playerCharacterController = new CharacterAirController(
                            i, this, playerService.ReturnPlayerScriptableObj(PlayerCharacterType.Air)
                            , weaponService, spawnCharacterPos
                        );
                    playerCharacterControllerList.Add(playerCharacterController);
                }
                else if (i == 1)
                {
                    PlayerCharacterController playerCharacterController = new CharacterWaterController(
                            i, this, playerService.ReturnPlayerScriptableObj(PlayerCharacterType.Water)
                            , weaponService, spawnCharacterPos
                        );
                    playerCharacterControllerList.Add(playerCharacterController);
                }
                else if (i == 2)
                {
                    PlayerCharacterController playerCharacterController = new CharacterFireController(
                            i, this, playerService.ReturnPlayerScriptableObj(PlayerCharacterType.Fire)
                            , weaponService, spawnCharacterPos
                        );
                    playerCharacterControllerList.Add(playerCharacterController);
                }
                spawnCharacterPos.x += 2;
            }
        }

        public void SetTurnId(string turnID)
        {
            this.turnID = turnID;
        }

        public string ReturnPlayerID()
        {
            return playerID;
        }

        public void SetShootInfo(float power, float angle, int characterID, bool gettingInput)
        {
            playerCharacterControllerList[characterID].SetShootInfo(power, angle, gettingInput);
        }

        public void DeactivateDisplayPanel()
        {
            for (int i = 0; i < playerCharacterControllerList.Count; i++)
            {
                playerCharacterControllerList[i].DeactivateInfoPanel();
            }
        }

        public Vector2 GetSpawnPos()
        {
            return fixedPos;
        }

    }
}